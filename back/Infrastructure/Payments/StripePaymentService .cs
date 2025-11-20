using Aplication.Interfaces.Payments;
using Core.Enums;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Payments
{
    public class StripePaymentService : IStripePaymentService
    {
        public StripePaymentService(IConfiguration configuration) {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        }
        public Task CancelSubscriptionAsync(string stripeSubscriptionId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateCustomerAsync (int userId)
        {
            var customerService = new CustomerService();

            var options = new CustomerCreateOptions
            {
             
                Metadata = new Dictionary<string, string>
            {
                { "UserId", userId.ToString() }
            }
            };

            var customer = await customerService.CreateAsync(options);

            return customer.Id; // returns StripeCustomerId
        }

        public async Task<string> CreateSubscriptionAsync(string stripeCustomerId, string stripePriceId)
        {
            var options = new SubscriptionCreateOptions
            {
                Customer = stripeCustomerId,
                Items = new List<SubscriptionItemOptions>
        {
            new SubscriptionItemOptions
            {
                Price = stripePriceId
            }
        }
            };

            var service = new SubscriptionService();
            Subscription subscription = await service.CreateAsync(options);

            return subscription.Id;
        }

        public Task HandleWebhookAsync(string json, string signature)
        {
            throw new NotImplementedException();
        }
    }
}
