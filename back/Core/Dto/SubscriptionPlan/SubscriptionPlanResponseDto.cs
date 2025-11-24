using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto.SubscriptionPlan
{
    public class SubscriptionPlanResponseDto
    {
        public string PlanType { get; set; }
        public string StripePriceId { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Interval { get; set; } 
        public List<string> Features { get; set; }
    }
}
