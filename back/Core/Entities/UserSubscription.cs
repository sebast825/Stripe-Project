using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class UserSubscription : ClassBase
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public string StripeSubscriptionId { get; set; }
        public string StripeCustomerId { get; set; }

        public SubscriptionPlanType Plan { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime CurrentPeriodEnd { get; set; }

        public SubscriptionStatus Status { get; set; }
    }
}
