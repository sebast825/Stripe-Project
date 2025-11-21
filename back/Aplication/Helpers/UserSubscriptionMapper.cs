using Aplication.Dto;
using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Helpers
{
    public static class UserSubscriptionMapper
    {

        public static StripeSubscriptionCreatedDto StripeSubscriptionCreatedDtoMapper(string subscriptionId,
            string customerId, DateTime startDate, string status)
        {
            SubscriptionStatus parsed;

            if (!Enum.TryParse(status, ignoreCase: true, out parsed))
                throw new ArgumentException($"Invalid subscription status: {status}");

            return new StripeSubscriptionCreatedDto
            {
                SubscriptionId = subscriptionId,
                CustomerId = customerId,
                StartDate = startDate,
                Status = parsed
            };
        }
    }
}
