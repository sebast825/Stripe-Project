using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.UserSubscription
{
    public class UserSubscriptionUpdateDto
    {
        public string StripeSubscriptionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CurrentPeriodEnd { get; set; }
        public string Status { get; set; }
        public bool CancelAtPeriodEnd { get; set; }
        public DateTime? CanceledAt { get; set; }
        public decimal Price { get; set; }

        
    }
}
