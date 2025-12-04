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
using System.Net.Sockets;
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

            int? userId = await _userRepository.GetIdByStripeCustomerIdAsync(userSubscriptionDto.CustomerId);
            if (userId == null)
                throw new InvalidOperationException(ErrorMessages.EntityNotFound("User", userSubscriptionDto.CustomerId));


            SubscriptionPlan plan = DemoPlans.GetByStripePriceId(userSubscriptionDto.PriceId);
            UserSubscription userSubscription = UserSubscriptionMapper.ToEntity(userId.Value, plan.PlanType, userSubscriptionDto);
            UserSubscription subscription = await _userSubscriptionRepository.AddAsync(userSubscription);

            return UserSubscriptionMapper.ToResponse(subscription);
        }

        public async Task<UserSubscriptionResponseDto> GetByUserId(int userId)
        {
            UserSubscription? subscription = await _userSubscriptionRepository.GetActiveSubscriptionByUserId(userId);
            if (subscription == null)
            {
                throw new KeyNotFoundException();

            }
            return UserSubscriptionMapper.ToResponse(subscription);

        }

        public async Task<UserSubscriptionResponseDto> UpdateAsync(UserSubscriptionUpdateDto updateDto, string customerId)
        {
            UserSubscription? subscription = await _userSubscriptionRepository.GetByStripeCustomerIdAsync(customerId);
            if (subscription == null)
            {
                throw new KeyNotFoundException(ErrorMessages.EntityNotFound("UserSubscription", $"customerId {customerId}"));
            }           
            if (subscription.StripeSubscriptionId != updateDto.StripeSubscriptionId)
            {
                throw new InvalidOperationException($"Entity StripeSubscriptionId {subscription.StripeSubscriptionId} doesn't match with dto id {updateDto.StripeSubscriptionId}");
            }
            SubscriptionPlanType plan = DemoPlans.GetByStripePriceId(updateDto.PriceId).PlanType;
            UserSubscriptionMapper.ApplySubscriptionUpdate(subscription, updateDto, plan);

            await _userSubscriptionRepository.UpdateAsync(subscription);
            return UserSubscriptionMapper.ToResponse(subscription);
        }
    }
}
