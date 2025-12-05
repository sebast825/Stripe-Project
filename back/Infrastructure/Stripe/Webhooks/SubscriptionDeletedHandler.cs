using Aplication.Dto;
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
using System.Threading.Tasks;

namespace Infrastructure.Stripe.Webhooks
{
    public class SubscriptionDeletedHandler : IStripeWebhookHandler
    {
        public string EventType => "customer.subscription.deleted";
        private readonly IUserSubscriptionService _userSubscriptionService;
        public SubscriptionDeletedHandler(IUserSubscriptionService userSubscriptionService)
        {
            _userSubscriptionService = userSubscriptionService;
        }
        public async Task HandleAsync(Event stripeEvent)
        {       
            UserSubscriptionUpdateDto userSubscriptionDto = UserSubscriptionMapper.ToUpdateDto(stripeEvent);
     
            var rsta = await _userSubscriptionService.UpdateAsync(userSubscriptionDto);
        }
    }
}
