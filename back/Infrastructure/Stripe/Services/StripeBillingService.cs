using Aplication.Interfaces.Stripe;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.BillingPortal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Stripe.Services
{
    public class StripeBillingService : IStripeBillingService
    {
        private readonly string _stripePrivateKey;

        public StripeBillingService(IConfiguration configuration)
        {

            _stripePrivateKey = configuration["Stripe:SecretKey"];
        }

        public async Task<string> CreateCustomerPortalUrlAsync(string stripeCustomerId)
        {
            var client = new StripeClient(_stripePrivateKey);

            var service = new SessionService(client);
            var options = new SessionCreateOptions
            {
                Customer = "cus_TUnIfVMzfyf58L",
            };
            var session = await service.CreateAsync(options);


            return session.Url;
        }
    }
}
