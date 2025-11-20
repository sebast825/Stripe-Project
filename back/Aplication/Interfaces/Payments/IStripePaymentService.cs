using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Payments
{
    public interface IStripePaymentService
    {
        Task<string> CreateSubscriptionAsync(string StripeCustomerId, string StripePriceId);
        Task CancelSubscriptionAsync(string stripeSubscriptionId);
        Task HandleWebhookAsync(string json, string signature);
        Task<string> CreateCustomerAsync(int userId);

    }

}
