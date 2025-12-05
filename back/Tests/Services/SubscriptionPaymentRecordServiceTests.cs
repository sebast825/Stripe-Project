using Aplication.Interfaces.Services;
using Aplication.Services;
using Castle.Core.Resource;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Stripe;
using Stripe.V2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = System.IO.File;

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


        [TestMethod]
        public async Task AddAsync__WhenInvoiceDoesNotExist_ShouldAddNewRecord()
        {
            //arrange
            Invoice invoice = LoadInvoiceFromFile("succeeded");
            int userId = 1;
            UserSubscription userSubscription = new UserSubscription
            {
                Id = userId
            };
            SubscriptionPaymentRecord subscriptionPaymentRecord = new SubscriptionPaymentRecord { };

            _userRepository.Setup(x =>x.GetIdByStripeCustomerIdAsync(invoice.CustomerId)).ReturnsAsync(userId);
            _userSubscriptionRepository.Setup(x => x.GetByStripeCustomerIdAsync(invoice.CustomerId)).ReturnsAsync(userSubscription);
            _subscriptionPaymentRecordRepository.Setup(x => x.GetByInvoiceId(invoice.Id)).ReturnsAsync((SubscriptionPaymentRecord)null);
            //act
            await _userSubscriptionService.AddAsync(invoice);
            //assert
            _subscriptionPaymentRecordRepository.Verify(x => x.AddAsync(It.IsAny<SubscriptionPaymentRecord>()), Times.Exactly(1));
           
        }

        [TestMethod]
        public async Task AddAsync__WhenInvoiceExist_ShouldUpdateRecord()
        {
            //arrange
            Invoice invoice = LoadInvoiceFromFile("succeeded");
            invoice.Created = DateTime.UtcNow.AddHours(2);
            int userId = 1;
            UserSubscription userSubscription = new UserSubscription
            {
                Id = userId
            };
            SubscriptionPaymentRecord subscriptionPaymentRecord = new SubscriptionPaymentRecord { };

            _userRepository.Setup(x => x.GetIdByStripeCustomerIdAsync(invoice.CustomerId)).ReturnsAsync(userId);
            _userSubscriptionRepository.Setup(x => x.GetByStripeCustomerIdAsync(invoice.CustomerId)).ReturnsAsync(userSubscription);
            _subscriptionPaymentRecordRepository.Setup(x => x.GetByInvoiceId(invoice.Id)).ReturnsAsync(subscriptionPaymentRecord);
            //act
            await _userSubscriptionService.AddAsync(invoice);
            //assert
            _subscriptionPaymentRecordRepository.Verify(x => x.AddAsync(It.IsAny<SubscriptionPaymentRecord>()), Times.Exactly(0));
            _subscriptionPaymentRecordRepository.Verify(x => x.UpdateAsync(It.IsAny<SubscriptionPaymentRecord>()), Times.Exactly(1));

        }
        [TestMethod]
        public async Task AddAsync__WhenInvoiceOutdated_ShouldThrow()
        {
            //arrange
            Invoice invoice = LoadInvoiceFromFile("succeeded");
            invoice.Created = DateTime.UtcNow.AddHours(-2);
            int userId = 1;
            UserSubscription userSubscription = new UserSubscription
            {
                Id = userId
            };
            SubscriptionPaymentRecord subscriptionPaymentRecord = new SubscriptionPaymentRecord { };

            _userRepository.Setup(x => x.GetIdByStripeCustomerIdAsync(invoice.CustomerId)).ReturnsAsync(userId);
            _userSubscriptionRepository.Setup(x => x.GetByStripeCustomerIdAsync(invoice.CustomerId)).ReturnsAsync(userSubscription);
            _subscriptionPaymentRecordRepository.Setup(x => x.GetByInvoiceId(invoice.Id)).ReturnsAsync(subscriptionPaymentRecord);
            //act
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _userSubscriptionService.AddAsync(invoice));
            //assert
            _subscriptionPaymentRecordRepository.Verify(x => x.AddAsync(It.IsAny<SubscriptionPaymentRecord>()), Times.Exactly(0));
            _subscriptionPaymentRecordRepository.Verify(x => x.UpdateAsync(It.IsAny<SubscriptionPaymentRecord>()), Times.Exactly(0));

        }
        public Invoice LoadInvoiceFromFile(string scenario)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string jsonPath = Path.Combine(baseDir, "Data", $"invoice-{scenario}.json");
            string json = File.ReadAllText(jsonPath);     
            return JsonConvert.DeserializeObject<Invoice>(json);

        }

    }
}
