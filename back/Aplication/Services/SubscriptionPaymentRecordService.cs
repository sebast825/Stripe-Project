using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class SubscriptionPaymentRecordService : ISubscriptionPaymentRecordService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;
        private readonly ISubscriptionPaymentRecordRepository _subscriptionPaymentRecordRepository;
   

        public SubscriptionPaymentRecordService(IUserRepository userRepository, IUserSubscriptionRepository userSubscriptionRepository, 
            ISubscriptionPaymentRecordRepository subscriptionPaymentRecordRepository)
        {
            _userRepository = userRepository;
            _userSubscriptionRepository = userSubscriptionRepository;
            _subscriptionPaymentRecordRepository = subscriptionPaymentRecordRepository;
        }

        public async Task AddAsync(Invoice invoice)
        {
            string customerId = invoice.CustomerId;
            string subscriptionId = invoice.Lines.Data.First().Parent.SubscriptionItemDetails.Subscription;

            int? userId = await _userRepository.GetIdByStripeCustomerId(customerId);
            if (userId == null) { 
                throw new ArgumentNullException(ErrorMessages.EntityNotFound(typeof(User).Name,customerId));
            }

            UserSubscription? userSubscription = await _userSubscriptionRepository.GetByStripeCustomerIdAsync(customerId);
            if (userSubscription == null)
            {
                throw new ArgumentNullException(ErrorMessages.EntityNotFound(typeof(UserSubscription).Name, customerId));
            }
            SubscriptionPaymentRecord entity = SubscriptionPaymentRecordMapper.ToEntity(invoice, userId.Value, userSubscription.Id);

            SubscriptionPaymentRecord? existingRecord = await _subscriptionPaymentRecordRepository.GetByInvoiceId(invoice.Id);
            if(existingRecord != null)
            {
                SubscriptionPaymentRecordMapper.ApplyUpdates(existingRecord, entity);
                await _subscriptionPaymentRecordRepository.UpdateAsync(existingRecord);
            }
            else
            {
                await _subscriptionPaymentRecordRepository.AddAsync(entity);

            }
        }
    }
}
