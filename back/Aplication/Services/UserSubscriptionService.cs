using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Core.Constants;
using Core.Dto.UserSubscription;
using Core.Entities;
using Core.Enums;
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
        private readonly IUserRepository _userRepository;

        public UserSubscriptionService(IUserSubscriptionRepository userSubscriptionRepository, IUserRepository userRepository)
        {
            _userSubscriptionRepository = userSubscriptionRepository;
            _userRepository = userRepository;
        }

        public async Task<UserSubscriptionResponseDto> AddAsync(StripeSubscriptionCreatedDto userSubscriptionDto)
        {
     
            int? userId = await _userRepository.GetIdByStripeCustomerId(userSubscriptionDto.CustomerId);
            if (userId == null) 
                throw new InvalidOperationException(ErrorMessages.EntityNotFound("User", userSubscriptionDto.CustomerId));

           
            SubscriptionPlan plan =   DemoPlans.GetByTypeByStripePriceId(userSubscriptionDto.PlanId);
            UserSubscription userSubscription = UserSubscriptionMapper.ToEntity(userId.Value, plan.PlanType, userSubscriptionDto);
            UserSubscription rsta = await _userSubscriptionRepository.AddAsync(userSubscription);

            return new UserSubscriptionResponseDto
            {
                Id = rsta.Id,
                Plan = rsta.Plan.ToString(),
                StartDate = rsta.StartDate,
                CurrentPeriodEnd = rsta.CurrentPeriodEnd,
                Status = rsta.Status.ToString()
            };

   
        }

        public Task HandleSubscriptionCreatedAsync(UserSubscription userSubscription)
        {
            throw new NotImplementedException();
        }
    }
}
