using Aplication.Dto;
using Core.Dto.UserSubscription;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Services
{
    public interface IUserSubscriptionService
    {
        Task<UserSubscriptionResponseDto> AddAsync(StripeSubscriptionCreatedDto userSubscriptionDto);

    }
}
