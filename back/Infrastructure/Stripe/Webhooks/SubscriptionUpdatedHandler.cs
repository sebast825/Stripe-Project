
using Aplication.Helpers;
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
            UserSubscriptionUpdateDto userSubscriptionDto = UserSubscriptionMapper.ToUpdateDto(subscription);

            var rsta = await _userSubscriptionService.UpdateAsync(userSubscriptionDto, subscription.CustomerId);

        }
    }
}
