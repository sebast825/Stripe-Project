using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Aplication.Services;
using Core.Dto.UserSubscription;
using Core.Entities;
using Core.Enums;
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

            var invoice = stripeEvent.Data.Object as Subscription;
            Console.WriteLine(invoice);

            var subscription = stripeEvent.Data.Object as Subscription;
            var asd = subscription.Items.Data.First().Plan.Id;

            // Datos esenciales a actualizar
            var subscriptionData = new UserSubscriptionUpdateDto
            {
                StripeSubscriptionId = subscription.Items.Data.First().Plan.Id,
                //StripeCustomerId = subscription.CustomerId,
                Status = subscription.Status,
                StartDate = subscription.Items.Data.First().CurrentPeriodStart,
                CurrentPeriodEnd = subscription.Items.Data.First().CurrentPeriodEnd,
                CancelAtPeriodEnd = subscription.CancelAtPeriodEnd,
                CanceledAt = subscription.CanceledAt.HasValue
                    ? subscription.CanceledAt.Value
                    : (DateTime?)null
            };


            Console.WriteLine("subscriptionData");

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(subscriptionData, new JsonSerializerOptions
            {
                WriteIndented = true
            }));


            var rsta = await _userSubscriptionService.UpdateAsync(subscriptionData, subscription.CustomerId);

        }
    }
}
