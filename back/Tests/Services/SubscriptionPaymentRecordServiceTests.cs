using Aplication.Interfaces.Services;
using Aplication.Services;
using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    [TestClass]
    public class SubscriptionPaymentRecordServiceTests
    {

        private Mock<IUserRepository> _userRepository;
        private Mock<IUserSubscriptionRepository> _userSubscriptionRepository;
        private Mock<ISubscriptionPaymentRecordRepository> _subscriptionPaymentRecordRepository;

        private SubscriptionPaymentRecordService _userSubscriptionService;
        [TestInitialize]
        public void Setup()
        {
            _userRepository = new Mock<IUserRepository>();
            _userSubscriptionRepository = new Mock<IUserSubscriptionRepository>();
            _subscriptionPaymentRecordRepository = new Mock<ISubscriptionPaymentRecordRepository>();
            _userSubscriptionService = new SubscriptionPaymentRecordService(_userRepository.Object, _userSubscriptionRepository.Object, _subscriptionPaymentRecordRepository.Object);
        }

        [TestMethod]
        public async Task GetUserSubscriptionIdByCustomerIdAsync_ValidCustomer_ReturnCustomerId()
        {
            //arrange
            string customerId = "cus_123";
            var mockSubscription = new UserSubscription
            {
                StripeCustomerId = customerId,
                Id = 1
            };
            _userSubscriptionRepository.
                Setup(r => r.GetByStripeCustomerIdAsync(customerId))
                .ReturnsAsync(mockSubscription);
            //act
            int subscritpionId = await _userSubscriptionService.GetUserSubscriptionIdByCustomerIdAsync(customerId);
            //assert
            Assert.AreEqual(mockSubscription.Id, subscritpionId);
            _userSubscriptionRepository.Verify(r => r.GetByStripeCustomerIdAsync(customerId), Times.Exactly(1));
        }

    }
}
