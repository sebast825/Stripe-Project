using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Payments;
using Core.Enums;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Stripe.Payments
{
    public class StripePaymentService : IStripePaymentService
    {
        public StripePaymentService(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        }
        public Task CancelSubscriptionAsync(string stripeSubscriptionId)
        {
            throw new NotImplementedException();
        }



        public async Task<string> CreateSubscriptionCheckoutSessionAsync(string stripeCustomerId, string stripePriceId)
        {

            var options = new SessionCreateOptions
            {
                Mode = "subscription",
                Customer = stripeCustomerId,
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    Price = stripePriceId,
                    Quantity = 1
                }
            },
                SuccessUrl = "https://localhost:5001/success",
                CancelUrl = "https://localhost:5001/cancel"

            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }

        public Task HandleWebhookAsync(string json, string signature)
        {
            throw new NotImplementedException();
        }

    }
}
