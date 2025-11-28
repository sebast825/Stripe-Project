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
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext __dataContext;

        public Repository(DataContext _dataContext)
        {
            __dataContext = _dataContext;
        }

        public async Task<T?> GetByIdAsync(int id)
            => await __dataContext.Set<T>().FindAsync(id);

        public async Task<IReadOnlyList<T>> ListAsync()
            => await __dataContext.Set<T>().ToListAsync();

        public async Task<T> AddAsync(T entity)
        {
            __dataContext.Set<T>().Add(entity);
            await __dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            __dataContext.Set<T>().Update(entity);
            await __dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            __dataContext.Set<T>().Remove(entity);
            await __dataContext.SaveChangesAsync();
        }
    }

}
