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
            SubscriptionStatus parsed = GetSubscriptionStatus(status);

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
        public static void ApplyUpdate(UserSubscription entity, UserSubscriptionUpdateDto dto, SubscriptionPlanType planType)
        {
            entity.StartDate = dto.StartDate;
            entity.CurrentPeriodEnd = dto.CurrentPeriodEnd;
            entity.Status = GetSubscriptionStatus(dto.Status);
            entity.CancelAtPeriodEnd = dto.CancelAtPeriodEnd;
            entity.CanceledAt = dto.CanceledAt;
            entity.StripeSubscriptionId = dto.StripeSubscriptionId;
            entity.Plan = planType;

        }


        public static SubscriptionStatus GetSubscriptionStatus(string status)
        {

            SubscriptionStatus parsedStatus;

            if (!Enum.TryParse(status, ignoreCase: true, out parsedStatus))
            {
                parsedStatus = SubscriptionStatus.Unknow;
            }
            return parsedStatus;
        }
    }
}
