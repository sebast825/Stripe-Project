using Aplication.Dto;
using Core.Dto;
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

        public async Task<PagedResult<User>> GetPagedAsync(int page, int pageSize, string? searchTerm)
        {
            int skipAmount = (page - 1) * pageSize;
            if (skipAmount < 0) skipAmount = 0;

            var query = _dataContext.Set<User>()
                .AsQueryable();

            if (searchTerm != null)
            {
                query = query.Where(x => x.FullName.Contains(searchTerm));
            }
            var totalItems = await query.CountAsync();
            var data = await query
              .Skip(skipAmount)
              .Take(pageSize)
              .ToListAsync();

            return new PagedResult<User>
            {
                Data = data,
                TotalItems = totalItems
            };
        }
    }
}
