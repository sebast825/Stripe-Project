using Aplication.Interfaces.Services;
using Core.Dto.UserSubscription;
using Core.Entities;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class UserSubscriptionService : IUserSubscriptionService
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        public UserSubscriptionService(IUserSubscriptionRepository userSubscriptionRepository)
        {
            _userSubscriptionRepository = userSubscriptionRepository;
        }

        public async Task<UserSubscriptionResponseDto> AddAsync(UserSubscription userSubscription)
        {
            UserSubscription rsta = await _userSubscriptionRepository.AddAsync(userSubscription);

            return new UserSubscriptionResponseDto
            {
                Id = rsta.Id,
                Plan = rsta.Plan.ToString(),
                CurrentPeriodEnd = rsta.CurrentPeriodEnd,
                Status = rsta.Status.ToString()
            };

   
        }
    }
}
