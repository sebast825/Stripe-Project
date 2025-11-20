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
    public class UserRepository : Repository<User>,IUserRepository 
    {
        private readonly DataContext _dataContext;
        public UserRepository(DataContext dataContext) :base(dataContext){
            _dataContext = dataContext;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dataContext.Set<User>()
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync();
        }


    }
}
