using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Payments;
using Aplication.Interfaces.Services;
using Aplication.Services;
using Core.Dto.User;
using Core.Dto.UserSubscription;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.UseCases.Subscriptions
{
    public class SubscribeUserUseCase
    {
        private readonly IStripePaymentService _stripePaymentService;
        private readonly IUserService _userService;

        public SubscribeUserUseCase(IStripePaymentService stripePaymentService, IUserService userService, IUserSubscriptionService userSubscriptionService)
        {
            _stripePaymentService = stripePaymentService;
            _userService = userService;
        }

        public async Task<string> ExecuteAsync(int userId, SubscriptionPlanType plan)
        {
            UserResponseDto user = await _userService.GetByIdAsync(userId);

            if (string.IsNullOrEmpty(user.StripeCustomerId))
            {
                string stripeCustomerId = await _stripePaymentService.CreateCustomerAsync(user.Id);
                await _userService.UpdateStripeCustomerId(user.Id, stripeCustomerId);
            }

            SubscriptionPlan planType = DemoPlans.GetByType(plan);
            string checkoutUrl = await _stripePaymentService.CreateSubscriptionCheckoutSessionAsync(user.StripeCustomerId, planType.StripePriceId);


            return checkoutUrl;

        }





    }

}
