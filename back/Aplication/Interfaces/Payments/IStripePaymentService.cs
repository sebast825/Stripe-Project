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

        Task<bool> CustomerHasPaymentMethodAsync(string stripeCustomerId);
        /// <summary>
        /// Creates a new Stripe subscription directly for a customer that already has
        /// a default payment method saved.
        /// 
        /// This method does NOT redirect the user or request card information.
        /// It immediately activates the subscription in Stripe.
        /// </summary>
        /// <param name="stripeCustomerId">The Stripe Customer ID.</param>
        /// <param name="stripePriceId">The Stripe Price ID representing the subscription plan.</param>
        /// <returns>
        /// The ID of the newly created Stripe Subscription.
        /// </returns>

        Task<string> CreateSubscriptionAsync(string stripeCustomerId, string stripePriceId);
        /// <summary>
        /// Creates a Stripe Checkout Session to collect payment method and start a subscription.
        /// Use when the customer doesn't have a saved payment method.
        /// </summary>
        /// <param name="stripeCustomerId">The Stripe Customer ID.</param>
        /// <param name="stripePriceId">The Stripe Price ID for the subscription plan.</param>
        /// <returns>URL to redirect the user to Stripe Checkout.</returns>

        Task<string> CreateSubscriptionCheckoutSessionAsync(string stripeCustomerId, string stripePriceId);        
        Task CancelSubscriptionAsync(string stripeSubscriptionId);
        Task HandleWebhookAsync(string json, string signature);
        /// <summary>
        /// Creates a new Stripe Customer using the user's identifying data.
        /// </summary>
        /// <returns>
        /// The Stripe Customer ID.
        /// </returns>

        Task<string> CreateCustomerAsync(int userId);

    }

}
