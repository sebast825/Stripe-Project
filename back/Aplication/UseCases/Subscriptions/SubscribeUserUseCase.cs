using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Payments;
using Aplication.Interfaces.Services;
using Aplication.Services;
using Core.Constants;
using Core.Dto.User;
using Core.Dto.UserSubscription;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Subscriptions
{
    public class SubscribeUserUseCase
    {
        private readonly IStripePaymentService _stripePaymentService;
        private readonly IUserService _userService;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public SubscribeUserUseCase(IStripePaymentService stripePaymentService, IUserService userService, IUserSubscriptionRepository userSubscriptionRepository)
        {
            _stripePaymentService = stripePaymentService;
            _userService = userService;
            _userSubscriptionRepository = userSubscriptionRepository;
        }

    
        public async Task<string> ExecuteAsync(int userId, int planId)
        {
            //if user already has a subscription throw exception
            UserSubscription? userSubscritpion =  await _userSubscriptionRepository.GetActiveSubscriptionByUserId(userId);
            if(userSubscritpion != null)
            {
                throw new ValidationException(ErrorMessages.UserAlreadyHasActiveSubscription);
            }

            UserResponseDto user = await _userService.GetByIdAsync(userId);            
            SubscriptionPlan planType = DemoPlans.GetById(planId);

            string checkoutUrl = await _stripePaymentService.CreateSubscriptionCheckoutSessionAsync(user.StripeCustomerId, planType.StripePriceId);

            return checkoutUrl;

        }

    }

}
