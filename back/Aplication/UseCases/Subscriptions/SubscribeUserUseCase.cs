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
        private readonly IUserSubscriptionService _userSubscriptionService;

        public SubscribeUserUseCase(IStripePaymentService stripePaymentService, IUserService userService, IUserSubscriptionService userSubscriptionService)
        {
            _stripePaymentService = stripePaymentService;
            _userService = userService;
            _userSubscriptionService = userSubscriptionService;
        }

       public async Task<SubscriptionFlowResultDto> ExecuteAsync(int userId, SubscriptionPlanType plan)
        {
            UserResponseDto user = await _userService.GetByIdAsync(userId);

            if (string.IsNullOrEmpty(user.StripeCustomerId))
            {
                string stripeCustomerId = await _stripePaymentService.CreateCustomerAsync(user.Id);
                await _userService.UpdateStripeCustomerId(user.Id, stripeCustomerId); 
            }

            SubscriptionPlan  planType = DemoPlans.GetByType(plan);
            SubscriptionFlowResultDto rsta = await CreateSubscriptionOrCheckoutSessionAsync(user.StripeCustomerId, planType.StripePriceId);

            if(rsta.FlowType == SubscriptionFlowType.subscribed)
            {
                var subscription = UserSubscriptionMapper.ToEntity(userId, plan, rsta.SubscriptionDto);
                 await _userSubscriptionService.AddAsync(subscription);
            }
            return rsta;
            
        

        }

        // UseCase decision
        public async Task<SubscriptionFlowResultDto> CreateSubscriptionOrCheckoutSessionAsync(string customerId, string priceId)
        {
            var hasPm = await _stripePaymentService.CustomerHasPaymentMethodAsync(customerId);

            if (hasPm)
            {
                StripeSubscriptionCreatedDto subscriptionDto = await _stripePaymentService.CreateSubscriptionAsync(customerId, priceId);

                return new SubscriptionFlowResultDto
                {
                    FlowType = SubscriptionFlowType.subscribed,
                    SubscriptionDto = subscriptionDto,

                }; 
            }
            else
            {
                string checkoutUrl = await _stripePaymentService.CreateSubscriptionCheckoutSessionAsync(customerId, priceId);
                return new SubscriptionFlowResultDto
                {
                    FlowType = SubscriptionFlowType.checkout,
                    CheckoutUrl = checkoutUrl,

                };
            }
        }



    }

}
