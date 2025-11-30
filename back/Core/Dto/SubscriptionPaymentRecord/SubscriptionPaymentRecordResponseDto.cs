using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.SubscriptionPaymentRecord
{
    public class SubscriptionPaymentRecordResponseDto
    {
        public long AmountPaid { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public string InvoiceUrl { get; set; }
        public DateTime PeriodEnd { get; set; }
        public DateTime PeriodStart { get; set; }
    }
}
