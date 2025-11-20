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

            SubscriptionPlan  planType = DemoPlans.GetPlanByName(plan.ToString());
            SubscriptionFlowResultDto rsta = await CreateSubscriptionOrCheckoutSessionAsync(user.StripeCustomerId, planType.StripePriceId);
            return rsta;

            //var subscription = new UserSubscription
            //{
            //    UserId = user.Id,
            //    StripeCustomerId = user.StripeCustomerId,
            //    StripeSubscriptionId = stripeSubId,
            //    Plan = plan,
            //    CurrentPeriodEnd = DateTime.UtcNow.AddMonths(1),
            //    Status = SubscriptionStatus.Active
            //};
           // return await _userSubscriptionService.AddAsync(subscription);

        }

        // UseCase decision
        public async Task<SubscriptionFlowResultDto> CreateSubscriptionOrCheckoutSessionAsync(string customerId, string priceId)
        {
            var hasPm = await _stripePaymentService.CustomerHasPaymentMethodAsync(customerId);

            if (hasPm)
            {
                string subscriptionId = await _stripePaymentService.CreateSubscriptionAsync(customerId, priceId);

                return new SubscriptionFlowResultDto
                {
                    FlowType = SubscriptionFlowType.subscribed,
                    SubscriptionId = subscriptionId,

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
