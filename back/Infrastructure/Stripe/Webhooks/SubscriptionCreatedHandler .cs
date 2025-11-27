using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Stripe.Webhooks
{
    public class SubscriptionCreatedHandler : IStripeWebhookHandler
    {
        public string EventType => "customer.subscription.created";
        private readonly IUserSubscriptionService _userSubscriptionService;
        public SubscriptionCreatedHandler(IUserSubscriptionService userSubscriptionService)
        {
            _userSubscriptionService = userSubscriptionService;
        }

          public async Task HandleAsync(Event stripeEvent)
        {
            var subscription = stripeEvent.Data.Object as Subscription;
            StripeSubscriptionCreatedDto subscriptionDto = UserSubscriptionMapper.ToCreateDto(subscription);
            await _userSubscriptionService.AddAsync(subscriptionDto);
        }
    }
}
