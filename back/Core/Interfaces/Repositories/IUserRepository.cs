using Core.Dto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);

        Task<int?> GetIdByStripeCustomerId(string stipeId);
        Task<PagedResult<User>> GetPagedAsync(int page, int pageSize, string? searchTerm);
}
}
