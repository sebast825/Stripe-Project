using Core.Constants;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    public static class DemoPlans
    {
        public static List<SubscriptionPlan> GetPlans()
        {
            return new List<SubscriptionPlan>{
                new SubscriptionPlan
                {
                    Id = 1,
                    Name = "Yearly",
                    Price = 1000m,
                    StripePriceId = "price_1SVGnvGjXgbUajlyJPFKyCJt",
                    Interval = "year"
                },
                new SubscriptionPlan
                {
                    Id = 2,
                    Name = "Monthly",
                    Price = 100m,
                    StripePriceId = "price_1SVGn7GjXgbUajlyIXwrBRKj",
                    Interval = "month"
                },
                new SubscriptionPlan
                {
                    Id = 3,
                    Name = "Daily",
                    Price = 15m,
                    StripePriceId = "price_1SVGndGjXgbUajly9H8F6VPm",
                    Interval = "day"
                }

            };
        }

        public static SubscriptionPlan GetPlanByName(string name)
        {
            return GetPlans().FirstOrDefault(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                   ?? throw new InvalidOperationException(ErrorMessages.EntityNotFound("SubscriptionPlan",name));
        }
    }

}
