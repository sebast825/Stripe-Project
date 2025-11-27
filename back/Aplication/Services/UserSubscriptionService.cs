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
            UserSubscription subscription = await _userSubscriptionRepository.AddAsync(userSubscription);

            return UserSubscriptionMapper.ToResponse(subscription);
        }

        public async Task<UserSubscriptionResponseDto> GetByUserId(int userId)
        {
            UserSubscription? subscription = await _userSubscriptionRepository.GetByUserId(userId);
            if (subscription == null)
            {
                throw new KeyNotFoundException();

            }
            return UserSubscriptionMapper.ToResponse(subscription);
       
        }

        public async Task<UserSubscriptionResponseDto> UpdateAsync(UserSubscriptionUpdateDto updateDto, string customerId)
        {
            UserSubscription? subscription = await _userSubscriptionRepository.GetByStripeCustomerIdAsync(customerId);
            if(subscription == null)
            {
                throw new KeyNotFoundException(ErrorMessages.EntityNotFound("UserSubscription",$"customerId {customerId}"));
            }
            SubscriptionPlanType plan = DemoPlans.GetByTypeByStripePriceId(updateDto.StripeSubscriptionId).PlanType;
            UserSubscriptionMapper.ApplySubscriptionUpdate(subscription, updateDto,plan);

            await _userSubscriptionRepository.UpdateAsync(subscription);
            return UserSubscriptionMapper.ToResponse(subscription);
        }
    }
}
