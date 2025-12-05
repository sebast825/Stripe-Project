using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Aplication.Interfaces.Stripe;
using Aplication.Services;
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
        private readonly ISubscriptionPaymentRecordService _subscriptionPaymentRecordService;
        public SubscriptionCreatedHandler(IUserSubscriptionService userSubscriptionService, ISubscriptionPaymentRecordService subscriptionPaymentRecordService)
        {
            _userSubscriptionService = userSubscriptionService;
            _subscriptionPaymentRecordService = subscriptionPaymentRecordService;
        }

        public async Task HandleAsync(Event stripeEvent)
        {
            var subscription = stripeEvent.Data.Object as Subscription;
            StripeSubscriptionCreatedDto subscriptionDto = UserSubscriptionMapper.ToCreateDto(subscription);
            await _userSubscriptionService.AddAsync(subscriptionDto);
            // Register initial payment to avoid race condition with invoice.payment_succeeded webhook
            var invoiceService = new InvoiceService();
            Invoice invoice = await invoiceService.GetAsync(subscription.LatestInvoiceId);
            await _subscriptionPaymentRecordService.AddAsync(invoice);
        }
    }
}
