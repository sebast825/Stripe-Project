using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class StripeWebhookService : IStripeWebhookService
    {
 
        private readonly ILogger<StripeWebhookService> _logger;
        private readonly IStripeWebhookFactory _webhookHandlerFactory;

        public StripeWebhookService(ILogger<StripeWebhookService> logger, IStripeWebhookFactory webhookHandlerFactory)
        {
            _logger = logger;
            _webhookHandlerFactory = webhookHandlerFactory;
        }

        public async Task ProcessAsync(Event stripeEvent)
        {
            try
            {
                _logger.LogInformation("Stripe webhook received: {Type}", stripeEvent.Type);

                IStripeWebhookHandler? handler = _webhookHandlerFactory.GetHandler(stripeEvent.Type);
                if (handler != null)
                {
                    await handler.HandleAsync(stripeEvent);
                }
            }
            catch (KeyNotFoundException ex)
            {
                LogError(ex, stripeEvent);
                return;
            }
            catch (InvalidOperationException ex)
            {
                LogError(ex, stripeEvent);
                return;
            }
            catch (ArgumentException ex)
            {
                LogError(ex, stripeEvent);
                return;
            }
            catch (Exception ex)
            {
                LogError(ex, stripeEvent);
                //throw exception and Stripe will retry
                throw;
            }

        }
        private void LogError(Exception ex, Event stripeEvent)
        {
            _logger.LogError(ex,
                 "Error processing Stripe event {EventType}. Event ID: {EventId}",
                 stripeEvent.Type,
                 stripeEvent.Id);
        }
    }
}
