
using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Core.Dto.UserSubscription;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Stripe.Webhooks
{
    public class SubscriptionUpdatedHandler : IStripeWebhookHandler
    {
        public string EventType => "customer.subscription.updated";
        private readonly IUserSubscriptionService _userSubscriptionService;
        public SubscriptionUpdatedHandler(IUserSubscriptionService userSubscriptionService)
        {
            _userSubscriptionService = userSubscriptionService;
        }
        public async Task HandleAsync(Event stripeEvent)
        {
            var subscription = stripeEvent.Data.Object as Subscription;

            var subscriptionData = new UserSubscriptionUpdateDto
            {
                StripeSubscriptionId = subscription.Items.Data.First().Plan.Id,
                Status = subscription.Status,
                StartDate = subscription.Items.Data.First().CurrentPeriodStart,
                CurrentPeriodEnd = subscription.Items.Data.First().CurrentPeriodEnd,
                CancelAtPeriodEnd = subscription.CancelAtPeriodEnd,
                CanceledAt = subscription.CanceledAt.HasValue
                    ? subscription.CanceledAt.Value
                    : (DateTime?)null
            };

            var rsta = await _userSubscriptionService.UpdateAsync(subscriptionData, subscription.CustomerId);



        }
    }
}
