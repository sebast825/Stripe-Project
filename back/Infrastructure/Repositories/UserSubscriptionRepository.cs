using Core.Entities;
using Core.Enums;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserSubscriptionRepository : Repository<UserSubscription> ,IUserSubscriptionRepository 
    {
        private readonly DataContext _dataContext;
        public UserSubscriptionRepository(DataContext dataContext): base(dataContext)
        {
            _dataContext = dataContext;
        }



        public async Task<UserSubscription?> GetByStripeCustomerIdAsync(string customerId)
        {
            return await _dataContext.Set<UserSubscription>()
             .Where(u => u.StripeCustomerId == customerId)
             .FirstOrDefaultAsync();
        }

        public async Task<UserSubscription?> GetActiveSubscriptionByUserId(int userId)
        {
            return await _dataContext.Set<UserSubscription>()
                .Where(u => u.UserId == userId && u.Status == SubscriptionStatus.Active)
                .FirstOrDefaultAsync();

        }
    }
}
