using Aplication.Interfaces.Stripe;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Stripe.Services
{
    public class StripeCustomerService : IStripeCustomerService
    {
        public StripeCustomerService(IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
        }
        public Task CancelSubscriptionAsync(string stripeSubscriptionId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CreateCustomerAsync(int userId)
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

    }
}
