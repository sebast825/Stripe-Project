using Core.Entities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    public static class SubscriptionPaymentRecordMapper
    {
        public static SubscriptionPaymentRecord toEntity(Invoice invoice, int userId, int userSubscriptionId)
        {
            string invoiceId = invoice.Id;
            string subscriptionId = invoice.Lines.Data.First().SubscriptionId;
            long atemptCount = invoice.AttemptCount;
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
            string invoiceUrl = invoice.HostedInvoiceUrl;
            string invoicePdf = invoice.InvoicePdf;

            return new SubscriptionPaymentRecord
            {
                InvoiceId = invoiceId,
                UserId = userId,
                UserSubscriptionId = userSubscriptionId,          
                AmountPaid = amountPaid,
                AmountTotal = amountTotal,
                Currency = currency,
                Status = status,
                PaidAt = paidAt,
                PeriodStart = periodStart,
                PeriodEnd = periodEnd,
                PaymentAttempts = atemptCount,
                InvoicePdf = invoicePdf,
                InvoiceUrl = invoiceUrl
            };
        }
    }
}
