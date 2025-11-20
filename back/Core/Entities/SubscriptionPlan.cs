using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SubscriptionPlan : ClassBase
    {
        public string Name { get; set; } 
        public string StripePriceId { get; set; } //
        public decimal Price { get; set; }
        public string Interval { get; set; } // daily, monthly, yearly
    }
}
