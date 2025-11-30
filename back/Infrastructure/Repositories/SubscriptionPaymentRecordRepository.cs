using Core.Dto;
using Core.Dto.User;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public  class SubscriptionPaymentRecordRepository : Repository<SubscriptionPaymentRecord>, ISubscriptionPaymentRecordRepository
    {
        private readonly DataContext _dataContext;
        public SubscriptionPaymentRecordRepository(DataContext dataContext) : base(dataContext)
        {            
            _dataContext = dataContext;            
        }

        public async Task<PagedResult<SubscriptionPaymentRecord?>> GetPagedByUserIdAsync(int userId, int page, int pageSize)
        {
            int skipAmount = (page - 1) * pageSize;
            if (skipAmount < 0) skipAmount = 0;

            var query = from u in _dataContext.SubscriptionPaymentRecords
                        where u.UserId == userId
                        select u;

            var totalItems = await query.CountAsync();

            var data = await query
              .OrderByDescending(x => x.CreatedAt)
              .Skip(skipAmount)
              .Take(pageSize)
              .AsNoTracking()
              .ToListAsync();

            return new PagedResult<SubscriptionPaymentRecord?>
            {
                Data = data,
                TotalItems = totalItems
            };
        }

        public async Task<SubscriptionPaymentRecord?>  GetByInvoiceId(string invoiceId)
        {
            {
                return await _dataContext.Set<SubscriptionPaymentRecord>()
                    .Where(x => x.InvoiceId == invoiceId)
                    .FirstOrDefaultAsync();
            }
        }

    }
}
