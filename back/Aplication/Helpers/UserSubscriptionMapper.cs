using Aplication.Dto;
using Core.Dto.UserSubscription;
using Core.Entities;
using Core.Enums;
using Stripe;
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

        public static StripeSubscriptionCreatedDto ToCreateDto(Subscription subscription)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            var item = subscription.Items?.Data?.FirstOrDefault();
            if (item == null)
                throw new InvalidOperationException("Subscription has no items.");
            SubscriptionStatus parsed = ParseSubscriptionStatus(subscription.Status);

            return new StripeSubscriptionCreatedDto
            {
                SubscriptionId = subscription.Id,
                CustomerId = subscription.CustomerId,
                Status = parsed,
                StartDate = item.CurrentPeriodStart,
                CurrentPeriodEnd = item.CurrentPeriodEnd,
                PlanId = item.Plan.Id,
                Price = GetUsdFromDecimal(item.Price.UnitAmountDecimal)

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
                Status = subscriptionDto.Status,
                Price = subscriptionDto.Price 
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
        public static void ApplySubscriptionUpdate(UserSubscription entity, UserSubscriptionUpdateDto dto, SubscriptionPlanType planType)
        {
            entity.StartDate = dto.StartDate;
            entity.CurrentPeriodEnd = dto.CurrentPeriodEnd;
            entity.Status = ParseSubscriptionStatus(dto.Status);
            entity.CancelAtPeriodEnd = dto.CancelAtPeriodEnd;
            entity.CanceledAt = dto.CanceledAt;
            entity.Price = dto.Price;
            entity.StripeSubscriptionId = dto.StripeSubscriptionId;
            entity.Plan = planType;

        }
        public static UserSubscriptionUpdateDto ToUpdateDto(Subscription subscription)
        {
            if (subscription == null)
                throw new ArgumentNullException(nameof(subscription));

            var item = subscription.Items?.Data?.FirstOrDefault();
            if (item == null)
                throw new InvalidOperationException("Subscription has no items.");

            return new UserSubscriptionUpdateDto
            {
                StripeSubscriptionId = item.Plan.Id,
                Status = subscription.Status,
                StartDate = item.CurrentPeriodStart,
                CurrentPeriodEnd = item.CurrentPeriodEnd,
                CancelAtPeriodEnd = subscription.CancelAtPeriodEnd,
                CanceledAt = subscription.CanceledAt ?? null,
                Price = GetUsdFromDecimal(item.Price.UnitAmountDecimal)

            };
        }




        public static SubscriptionStatus ParseSubscriptionStatus(string status)
        {

            SubscriptionStatus parsedStatus;

            if (!Enum.TryParse(status, ignoreCase: true, out parsedStatus))
            {
                parsedStatus = SubscriptionStatus.Unknow;
            }
            return parsedStatus;
        }


        private static decimal GetUsdFromDecimal(decimal? price)
        {
            //the price may be undefined depending of wich kind of billing we are using
            if (price == null) return 0;
            return price.Value / 100m;
        }
    }
}
