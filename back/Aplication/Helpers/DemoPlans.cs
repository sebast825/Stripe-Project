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
        private static List<SubscriptionPlan> GetPlans()
        {
            return new List<SubscriptionPlan>{
                new SubscriptionPlan
                {
                    Id = 1,
                    PlanType = SubscriptionPlanType.Galería,
                    Price = 500m,
                    StripePriceId = "price_1SZwFrGjXgbUajlyremR5Js7",
                    Interval = "month",
                    Name = "Galería",
                  Description = "Todo para exponer, pulir y mostrar tu trabajo con calidad profesional.",
                Features = new List<string>
                {
                    "Acceso a herramientas avanzadas para acabado profesional",
                    "Optimización de obras para exhibición y presentación",
                    "Gestión completa de tu portafolio creativo",
                    "Recursos premium orientados a impacto visual y detalle"
                }

                },
                new SubscriptionPlan
                {
                    Id = 2,
                    PlanType = SubscriptionPlanType.Lienzo,
                    Price = 300m,
                    StripePriceId = "price_1SZwFfGjXgbUajlyeYiy6QG0",
                    Interval = "month",
                    Name = "Lienzo",
                    Description = "Más herramientas y espacio para desarrollar tu obra.",
                    Features = new List<string>
                    {
                        "Área de trabajo ampliada para proyectos en evolución",
                        "Herramientas intermedias para estructurar y pulir tus ideas",
                        "Capacidad para gestionar varias piezas en simultáneo",
                        "Opciones de color y detalle más avanzadas"
                    }

                },
                new SubscriptionPlan
                {
                    Id = 3,
                    PlanType = SubscriptionPlanType.Boceto,
                    Price = 100m,
                    StripePriceId = "price_1SZwFOGjXgbUajly98vAJit8",
                    Interval = "month",
                    Name = "Boceto",
                    Description = "Lo esencial para comenzar a trazar tus primeras ideas.",
                    Features = new List<string>
                   {
                        "Espacio creativo básico para explorar conceptos",
                        "Herramientas iniciales para experimentar sin presión",
                        "Ideal para quienes comienzan a delinear sus primeras obras",
                        "Permite guardar y revisar tus primeros trazos"
                    }
                }

            };
        }


        public static SubscriptionPlan GetById(int id)
        {
            return GetPlans().FirstOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException(ErrorMessages.EntityNotFound(nameof(SubscriptionPlan), id));
        }
        public static SubscriptionPlan GetByStripePriceId(string priceId)
        {
            return GetPlans().FirstOrDefault(p => p.StripePriceId.Equals(priceId))
                ?? throw new InvalidOperationException(ErrorMessages.EntityNotFound(nameof(SubscriptionPlan), priceId));
        }
    }

}
