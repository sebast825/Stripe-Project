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

        public async Task HandleAsync(Event stripeEvent)
        {

            var invoice = stripeEvent.Data.Object as Invoice;
            string invoiceId = invoice.Id;

            string subscriptionId = invoice.Lines.Data.First().SubscriptionId;
            long atemptCount = invoice.AttemptCount;

            string customerId = invoice.CustomerId;
            long amountPaid = invoice.AmountPaid;
            string currency = invoice.Currency;
            long amountTotal = invoice.Total;
            string status = invoice.Status;

            // May be null if the event is very old
            DateTime? paidAt = null;
            if (invoice.StatusTransitions?.PaidAt != null)
            {
                paidAt = invoice.StatusTransitions.PaidAt;
            }

            var subLine = invoice.Lines?.Data
                .FirstOrDefault(x => x.Period != null);

            if (subLine == null)
                throw new InvalidOperationException(
                    "Missing billing period: subscription invoice received without a valid Start/End period."
                );

            var periodStart = subLine.Period.Start;
            var periodEnd = subLine.Period.End;
            long? attempts = invoice.AttemptCount;
            string invoiceUrl = invoice.HostedInvoiceUrl;
            string invoicePdf = invoice.InvoicePdf;
            SubscriptionPaymentRecord asd = new SubscriptionPaymentRecord
            {
                InvoiceId = invoiceId,
                //UserId
                //SbscriptionId
                AmountPaid = amountPaid,
                AmountTotal = amountTotal,
                Currency = currency,
                Status = status,
                PaidAt = paidAt,
                PeriodStart = periodStart,
                PeriodEnd = periodEnd,
                PaymentAttempts = attempts,
                InvoicePdf = invoicePdf,
                InvoiceUrl = invoiceUrl
            };

        }
       
    }
}
