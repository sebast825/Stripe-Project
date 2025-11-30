using Aplication.Dto;
using Core.Dto.SubscriptionPaymentRecord;
using Core.Entities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Services
{
    public interface ISubscriptionPaymentRecordService
    {
        Task AddAsync(Invoice invoice);
        Task<PagedResponseDto<SubscriptionPaymentRecordResponseDto>> GetPaymentsByUserIdAsync(int userId, int page, int pageSize);
    }
}
