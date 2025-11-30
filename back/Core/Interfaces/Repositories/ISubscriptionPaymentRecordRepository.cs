using Core.Dto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface ISubscriptionPaymentRecordRepository : IRepository<SubscriptionPaymentRecord>
    {
        Task<SubscriptionPaymentRecord?> GetByInvoiceId(string invoiceId);
        Task<PagedResult<SubscriptionPaymentRecord?>> GetPagedByUserIdAsync(int userId, int page, int pageSize);

    }
}
