using Aplication.Dto;
using Core.Dto;
using Core.Dto.User;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<int> CountAsync(string? searchTerm)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dataContext.Set<User>()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }

        public async Task<int?> GetIdByStripeCustomerId(string stipeId)
        {
            return await _dataContext.Set<User>()
                .AsNoTracking()
                .Where(u => u.StripeCustomerId == stipeId)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<UserWithSubscriptionResponseDto>> GetPagedAsync(int page, int pageSize, string? searchTerm)
        {
            int skipAmount = (page - 1) * pageSize;
            if (skipAmount < 0) skipAmount = 0;


            var query = from u in _dataContext.Users
                        join s in _dataContext.UserSubscriptions on u.StripeCustomerId equals s.StripeCustomerId
                        into subs
                        let sub = subs.OrderByDescending(x => x.CreatedAt).FirstOrDefault()
                        select new UserWithSubscriptionResponseDto { Id = u.Id, FullName = u.FullName, Plan = sub.Plan, Status = sub.Status };


            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => (x.FullName).Contains(searchTerm));
            }

            var totalItems = await query.CountAsync();
            var data = await query
              .OrderBy(x => x.FullName)
              .Skip(skipAmount)
              .Take(pageSize)
              .AsNoTracking()
              .ToListAsync();

            return new PagedResult<UserWithSubscriptionResponseDto>
            {
                Data = data,
                TotalItems = totalItems
            };
        }
    }
}
