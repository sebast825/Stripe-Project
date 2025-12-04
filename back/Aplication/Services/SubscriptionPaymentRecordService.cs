using Aplication.Dto;
using Aplication.Helpers;
using Aplication.Interfaces.Services;
using Core.Constants;
using Core.Dto;
using Core.Dto.SubscriptionPaymentRecord;
using Core.Entities;
using Core.Interfaces.Repositories;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            string subscriptionId = invoice.Lines.Data.First().Parent.SubscriptionItemDetails.Subscription;

            int userId = await GetUserIdByCustomerIdAsync(invoice.CustomerId);
            int userSubscriptionId = await GetUserSubscriptionIdByCustomerIdAsync(invoice.CustomerId);

            SubscriptionPaymentRecord entity = SubscriptionPaymentRecordMapper.ToEntity(invoice, userId, userSubscriptionId);
            SubscriptionPaymentRecord? existingRecord = await _subscriptionPaymentRecordRepository.GetByInvoiceId(invoice.Id);

            if (existingRecord != null)
            {
                SubscriptionPaymentRecordMapper.ApplyUpdates(existingRecord, entity);
                await _subscriptionPaymentRecordRepository.UpdateAsync(existingRecord);
            }
            else
            {
                await _subscriptionPaymentRecordRepository.AddAsync(entity);
            }
        }
        private async Task<int> GetUserIdByCustomerIdAsync(string customerId)
        {
            int? userId = await _userRepository.GetIdByStripeCustomerIdAsync(customerId);
            if (userId == null)
            {
                throw new ArgumentNullException(ErrorMessages.EntityNotFound(typeof(User).Name, customerId));
            }
            return userId.Value;
        }

        internal async Task<int> GetUserSubscriptionIdByCustomerIdAsync(string customerId)
        {
            UserSubscription? userSubscription = await _userSubscriptionRepository.GetByStripeCustomerIdAsync(customerId);
            if (userSubscription == null)
            {
                throw new ArgumentNullException(ErrorMessages.EntityNotFound(typeof(UserSubscription).Name, customerId));
            }
            return userSubscription.Id;
        }

        public async Task<PagedResponseDto<SubscriptionPaymentRecordResponseDto>> GetPaymentsByUserIdAsync(int userId, int page, int pageSize)
        {
            PagedResult<SubscriptionPaymentRecord?> rsta = await _subscriptionPaymentRecordRepository.GetPagedByUserIdAsync(userId, page, pageSize);
            List<SubscriptionPaymentRecordResponseDto> dataMapped = rsta.Data.Select(x => SubscriptionPaymentRecordMapper.ToResponse(x)).ToList();
            return PagedMapper.ToResponse(page, pageSize, rsta.TotalItems, dataMapped);                
        }
    }
}

