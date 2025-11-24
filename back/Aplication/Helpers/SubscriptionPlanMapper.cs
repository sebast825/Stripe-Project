using Aplication.Dto;
using Core.Dto.SubscriptionPlan;
using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    public static class SubscriptionPlanMapper
    {

        public static SubscriptionPlanResponseDto ToResponse(SubscriptionPlan entity)
        {
           

            return new SubscriptionPlanResponseDto
            {
                Id = entity.Id,
                PlanType = entity.PlanType.ToString(),
                Price = entity.Price,
                Name = entity.Name,
                Description = entity.Description,
                Interval = entity.Interval,
                Features = entity.Features
            };
        }
      
    }
}
