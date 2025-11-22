using Aplication.Interfaces.Services;
using Core.Dto.User;
using Core.Entities;
using Infrastructure.Stripe.Webhooks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Cryptography.Xml;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        private readonly string _stripeSignatureKey;
        private readonly WebhookHandlerFactory _webhookFactory;
        public WebhookController(IConfiguration configuration, WebhookHandlerFactory webhookFactory)
        {
            _stripeSignatureKey = configuration["Stripe:Signature"];
            _webhookFactory = webhookFactory;
        }
        [HttpPost]
        public async Task<ActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var headerSignature = Request.Headers["Stripe-Signature"];


            Event stripeEvent;

            try
            {
                //transfor to validate the signature is valid
                stripeEvent = EventUtility.ConstructEvent(json, headerSignature, _stripeSignatureKey);

                IStripeWebhookHandler handler = _webhookFactory.GetHandler(stripeEvent.Type);
                if (handler != null)
                {
                    await handler.HandleAsync(stripeEvent);
                }
                else
                {
                    Console.WriteLine(stripeEvent.Type);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }

            return Ok();


        }
    }
}
