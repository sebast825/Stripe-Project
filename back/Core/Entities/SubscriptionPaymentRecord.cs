using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SubscriptionPaymentRecord : ClassBase
    {
        public string InvoiceId { get; set; }         
        public int UserId { get; set; }               
        public User User { get; set; }
        public UserSubscription Subscription { get; set; }
        public int SubscriptionId { get; set; }    
        public long AmountPaid { get; set; }
        public long AmountTotal { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public long? PaymentAttempts { get; set; }
        public string InvoiceUrl { get; set; } // Public URL for the customer to view the invoice
        public string InvoicePdf { get; set; } // Direct URL to the PDF version of the invoice (for internal records)
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
