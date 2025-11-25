using Core.Constants;
using Core.Dto.SubscriptionPlan;
using Core.Dto.UserSubscription;
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
        public static List<SubscriptionPlanResponseDto> GetPlanResponse()
        {
            return GetPlans().Select(plan => SubscriptionPlanMapper.ToResponse(plan)).ToList();

        }
        public static List<SubscriptionPlan> GetPlans()
        {
            return new List<SubscriptionPlan>{
                new SubscriptionPlan
                {
                    Id = 1,
                    PlanType = SubscriptionPlanType.Yearly,
                    Price = 1000m,
                    StripePriceId = "price_1SVGnvGjXgbUajlyJPFKyCJt",
                    Interval = "year",
                    Name = "Anual",
                    Description = "Ideal para usuarios que buscan el mejor precio a largo plazo.",
                    Features = new List<string>
                    {
                        "Acceso ilimitado a todas las funciones",
                        "Soporte prioritario",
                        "Actualizaciones incluidas",
                        "Ahorro anual respecto al plan mensual"
                    }
                },
                new SubscriptionPlan
                {
                    Id = 2,
                    PlanType = SubscriptionPlanType.Monthly,
                    Price = 100m,
                    StripePriceId = "price_1SVGn7GjXgbUajlyIXwrBRKj",
                    Interval = "month",
                    Name = "Mensual",
                    Description = "La opción más equilibrada para uso continuo sin compromiso anual.",
                    Features = new List<string>
                    {
                        "Acceso completo a todas las funciones",
                        "Actualizaciones incluidas",
                        "Costo mensual fijo"
                    }
                },
                new SubscriptionPlan
                {
                    Id = 3,
                    PlanType = SubscriptionPlanType.Daily,
                    Price = 15m,
                    StripePriceId = "price_1SVGndGjXgbUajly9H8F6VPm",
                    Interval = "day",
                    Name = "Diario",
                    Description = "Perfecto para uso puntual o de corta duración.",
                    Features = new List<string>
                    {
                        "Acceso completo por 24 horas",
                        "Sin compromisos",
                        "Ideal para usuarios ocasionales"
                    }
                }

            };
        }

        public static SubscriptionPlan GetByType(SubscriptionPlanType plan)
        {
            return GetPlans().FirstOrDefault(p => p.PlanType.Equals(plan))
                ?? throw new InvalidOperationException(ErrorMessages.EntityNotFound("SubscriptionPlan", plan.ToString()));
        }

        public static SubscriptionPlan GetById(int planId)
        {
            return GetPlans().FirstOrDefault(p => p.Id == planId)
                ?? throw new InvalidOperationException(ErrorMessages.EntityNotFound("SubscriptionPlan", planId));
        }
        public static SubscriptionPlan GetByTypeByStripePriceId(string stripeId)
        {
            return GetPlans().FirstOrDefault(p => p.StripePriceId.Equals(stripeId))
                ?? throw new InvalidOperationException(ErrorMessages.EntityNotFound("SubscriptionPlan", stripeId));
        }
    }

}
