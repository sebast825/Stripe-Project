using Aplication.Dto;
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

        /// <summary>
        /// Creates a Stripe Checkout Session to collect payment method and start a subscription.
        /// Use when the customer doesn't have a saved payment method.
        /// </summary>
        /// <param name="stripeCustomerId">The Stripe Customer ID.</param>
        /// <param name="stripePriceId">The Stripe Price ID for the subscription plan.</param>
        /// <returns>URL to redirect the user to Stripe Checkout.</returns>
        Task<string> CreateSubscriptionCheckoutSessionAsync(string stripeCustomerId, string stripePriceId);        
        Task HandleWebhookAsync(string json, string signature);

        Task CancelSubscriptionAsync(string stripeSubscriptionId);

    }

}
