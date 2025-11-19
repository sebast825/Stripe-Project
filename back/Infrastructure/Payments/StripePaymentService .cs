using Aplication.Interfaces.Payments;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Payments
{
    public class StripePaymentService : IStripePaymentService
    {
        public Task CancelSubscriptionAsync(string stripeSubscriptionId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateCustomerIfNotExistsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateSubscriptionAsync(int userId, SubscriptionPlanType plan)
        {
            throw new NotImplementedException();
        }

        public Task HandleWebhookAsync(string json, string signature)
        {
            throw new NotImplementedException();
        }
    }
}
