using Aplication.Dto;
using Core.Dto.UserSubscription;
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
    public static class UserSubscriptionMapper
    {

        public static StripeSubscriptionCreatedDto FromStripe(string subscriptionId,
            string customerId, DateTime startDate, string status, string planId, DateTime? currentPeriodEnd = null)
        {
            SubscriptionStatus parsed;

            if (!Enum.TryParse(status, ignoreCase: true, out parsed))
                throw new ArgumentException($"Invalid subscription status: {status}");

            return new StripeSubscriptionCreatedDto
            {
                SubscriptionId = subscriptionId,
                CustomerId = customerId,
                StartDate = startDate,
                CurrentPeriodEnd = currentPeriodEnd,
                Status = parsed,
                PlanId = planId
            };
        }
        public static UserSubscription ToEntity(int userId, SubscriptionPlanType plan, StripeSubscriptionCreatedDto subscriptionDto)
        {
            return new UserSubscription
            {
                UserId = userId,
                StripeCustomerId = subscriptionDto.CustomerId,
                StripeSubscriptionId = subscriptionDto.SubscriptionId,
                StartDate = subscriptionDto.StartDate,
                CurrentPeriodEnd = subscriptionDto.CurrentPeriodEnd,
                Plan = plan,
                Status = subscriptionDto.Status
            }
            ;
        }
        public static UserSubscriptionResponseDto ToResponse(UserSubscription entity)
        {
            return new UserSubscriptionResponseDto
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                CurrentPeriodEnd = entity.CurrentPeriodEnd,
                Plan = entity.Plan.ToString(),
                Status = entity.Status.ToString()
            }
            ;
        }
    }
}
