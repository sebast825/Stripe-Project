using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Aplication.Services;
using Aplication.UseCases.Auth;
using Core.Dto.UserSubscription;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    [TestClass]
    public class UserSubscriptionServiceTests
    {
        private Mock<IUserSubscriptionRepository> _userSubscriptionRepository;
        private Mock<IUserRepository> _userRepository;
        private IUserSubscriptionService _userSubscriptionService;

        [TestInitialize]
        public void SetUp()
        {
            _userSubscriptionRepository = new Mock<IUserSubscriptionRepository>();
            _userRepository = new Mock<IUserRepository>();
            _userSubscriptionService = new UserSubscriptionService(
            _userSubscriptionRepository.Object,
            _userRepository.Object
                );
        }

        [TestMethod]
        public async Task UpdateAsync_ValidInput_UpdatesSuccessfully()
        {
            User user = new User
            {
                Email = "test@gmail.com",
                Password = "test",
                StripeCustomerId = "cus_123"
            };

            SubscriptionPlan currentPlan = DemoPlans.GetById(1);
            SubscriptionPlan newPlan = DemoPlans.GetById(2);


            UserSubscription existingSubscription = new UserSubscription
            {
                StripeSubscriptionId = "sub_!23",
                Plan = currentPlan.PlanType,
                Status = SubscriptionStatus.Active,
                StripeCustomerId = user.StripeCustomerId,
                
            };
            UserSubscriptionUpdateDto newSubscritionDto = new UserSubscriptionUpdateDto
            {
                StripeSubscriptionId = "sub_!23",
                PriceId = newPlan.StripePriceId,
                Status = "active",
            };

            _userSubscriptionRepository.Setup(r => r.GetByStripeCustomerIdAsync(user.StripeCustomerId))
                    .Returns(Task.FromResult(existingSubscription));

            UserSubscriptionResponseDto rsta = await _userSubscriptionService.UpdateAsync(newSubscritionDto, user.StripeCustomerId);

            _userSubscriptionRepository.Verify(s => s.UpdateAsync(It.IsAny<UserSubscription>()), Times.Once);
            Assert.AreEqual(rsta.Plan.ToString(),newPlan.Name);
        }

        [TestMethod]
        public async Task UpdateAsync_UserSubscritionNotFound_Throw()
        {
            User user = new User
            {
                Email = "test@gmail.com",
                Password = "test",
                StripeCustomerId = "cus_123"
            };

            SubscriptionPlan newPlan = DemoPlans.GetById(2);
   
            UserSubscriptionUpdateDto newSubscritionDto = new UserSubscriptionUpdateDto
            {
                StripeSubscriptionId = "sub_!23",
                PriceId = newPlan.StripePriceId,
                Status = "active",
            };

            _userSubscriptionRepository.Setup(r => r.GetByStripeCustomerIdAsync(user.StripeCustomerId))
                    .ReturnsAsync((UserSubscription)null);

            await Assert.ThrowsExceptionAsync<KeyNotFoundException>(() => _userSubscriptionService.UpdateAsync(newSubscritionDto, user.StripeCustomerId));

            _userSubscriptionRepository.Verify(s => s.GetByStripeCustomerIdAsync(It.IsAny<string>()), Times.Once);
     
        }
        
    }
}
