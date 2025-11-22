using Core.Constants;
using Core.Entities;
using Core.Enums;
using Microsoft.Extensions.Primitives;
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
                    PlanType = SubscriptionPlanType.Yearly,
                    Price = 1000m,
                    StripePriceId = "price_1SVGnvGjXgbUajlyJPFKyCJt",
                    Interval = "year"
                },
                new SubscriptionPlan
                {
                    Id = 2,
                    PlanType = SubscriptionPlanType.Monthly,
                    Price = 100m,
                    StripePriceId = "price_1SVGn7GjXgbUajlyIXwrBRKj",
                    Interval = "month"
                },
                new SubscriptionPlan
                {
                    Id = 3,
                    PlanType = SubscriptionPlanType.Daily,
                    Price = 15m,
                    StripePriceId = "price_1SVGndGjXgbUajly9H8F6VPm",
                    Interval = "day"
                }

            };
        }

        public static SubscriptionPlan GetByType(SubscriptionPlanType plan)
        {
            return GetPlans().FirstOrDefault(p => p.PlanType.Equals(plan))
                ?? throw new InvalidOperationException(ErrorMessages.EntityNotFound("SubscriptionPlan", plan.ToString()));
        }
        public static SubscriptionPlan GetByTypeByStripePriceId(string stripeId)
        {
            return GetPlans().FirstOrDefault(p => p.StripePriceId.Equals(stripeId))
                ?? throw new InvalidOperationException(ErrorMessages.EntityNotFound("SubscriptionPlan", stripeId));
        }
    }

}
