using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Core.Dto.UserSubscription;
using Core.Entities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Stripe.Webhooks
{
    public class InvoicePaymentSuccededHandler : IStripeWebhookHandler
    {
        public string EventType => "invoice.payment_succeeded";
        private readonly ISubscriptionPaymentRecordService _subscriptionPaymentRecordService;

        public InvoicePaymentSuccededHandler(ISubscriptionPaymentRecordService subscriptionPaymentRecordService)
        {
            _subscriptionPaymentRecordService = subscriptionPaymentRecordService;
        }
        public async Task HandleAsync(Event stripeEvent)
        {

            var invoice = stripeEvent.Data.Object as Invoice;
            await _subscriptionPaymentRecordService.AddAsync(invoice);

        }
       
    }
}
