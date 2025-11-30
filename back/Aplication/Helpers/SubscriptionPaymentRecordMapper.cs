using Core.Dto.SubscriptionPaymentRecord;
using Core.Entities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    public static class SubscriptionPaymentRecordMapper
    {
 
        public static SubscriptionPaymentRecord ApplyUpdates(SubscriptionPaymentRecord existing, SubscriptionPaymentRecord source) 
        {
            existing.Status = source.Status;
            existing.AmountPaid = source.AmountPaid;
            existing.AmountTotal = source.AmountTotal;
            existing.InvoicePdf = source.InvoicePdf;
            existing.InvoiceUrl = source.InvoiceUrl;
            existing.PeriodEnd = source.PeriodEnd;
            existing.PeriodStart = source.PeriodStart;
            existing.PaymentAttempts = source.PaymentAttempts;
            existing.PaidAt = source.PaidAt;
            existing.UpdatedAt = DateTime.UtcNow;
            return existing;
        }
        public static SubscriptionPaymentRecord ToEntity(Invoice invoice, int userId, int userSubscriptionId)
        {
            string subscriptionId = invoice.Lines.Data.First().SubscriptionId;          

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
         

            return new SubscriptionPaymentRecord
            {
                InvoiceId = invoice.Id,
                UserId = userId,
                UserSubscriptionId = userSubscriptionId,          
                AmountPaid = invoice.AmountPaid,
                AmountTotal = invoice.Total,
                Currency = invoice.Currency,
                Status = invoice.Status,
                PaidAt = paidAt,
                PeriodStart = periodStart,
                PeriodEnd = periodEnd,
                PaymentAttempts = invoice.AttemptCount,
                InvoicePdf = invoice.InvoicePdf,
                InvoiceUrl = invoice.HostedInvoiceUrl
            };

          

        }
        public static SubscriptionPaymentRecordResponseDto ToResponse(SubscriptionPaymentRecord entitty)
        {
            return new SubscriptionPaymentRecordResponseDto
            {
                AmountPaid = entitty.AmountPaid,
                Currency = entitty.Currency,
                Status = entitty.Status,
                PaidAt = entitty.PaidAt,
                InvoiceUrl = entitty.InvoiceUrl,
                PeriodEnd = entitty.PeriodEnd,
                PeriodStart = entitty.PeriodStart
            };
        }
    }
}
