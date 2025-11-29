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
