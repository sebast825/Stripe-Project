using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Aplication.Services;
using Core.Entities;
using Core.Enums;
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
        public string EventType => "invoice.payment_succeeded";
        private readonly IUserSubscriptionService _userSubscriptionService;
        public SubscriptionCreatedHandler(IUserSubscriptionService userSubscriptionService)
        {
            _userSubscriptionService = userSubscriptionService;
        }
        public async Task HandleAsync(Event stripeEvent)
        {

            var invoice = stripeEvent.Data.Object as Invoice;

            if (invoice != null)
            {
                var subscriptionId = invoice.Lines.First().Parent.SubscriptionItemDetails.Subscription;
                var customerId = invoice.CustomerId;
                var startDate = invoice.Created;
                var currentPeriodEnd = invoice.Lines?.Data?.FirstOrDefault()?.Period?.End;
  
                var planId = "";
                if (invoice.Lines?.Data?.Count > 0)
                { 
                    var lineItem = invoice.Lines.Data.First();

                    planId = lineItem.Pricing.PriceDetails.Price;
                }
                var subscriptionData = new
                {
                    StripeSubscriptionId = subscriptionId,
                    StripeCustomerId = customerId,
                    StartDate = invoice.Created,
                    Plan = planId,
                    CurrentPeriodEnd = currentPeriodEnd
                };

                StripeSubscriptionCreatedDto subscriptionDto = UserSubscriptionMapper.FromStripe(subscriptionId,
                    customerId, startDate,"Active" ,  planId, currentPeriodEnd);

                var rsta = await _userSubscriptionService.AddAsync(subscriptionDto);



                Console.WriteLine(rsta);
            }




            Console.WriteLine("Recive evento");
            //Console.WriteLine(stripeEvent);
           // return Task.CompletedTask;
        }
    }
}
