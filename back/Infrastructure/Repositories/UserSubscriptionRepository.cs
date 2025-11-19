using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly DataContext _dataContext;
        public UserSubscriptionRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(UserSubscription userSubscriptions)
        {
            await _dataContext.Set<UserSubscription>().AddAsync(userSubscriptions);
            await _dataContext.SaveChangesAsync();
        }
    }
}
