using Mapping.Data.Contexts;
using Mapping.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mapping.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
#pragma warning disable
        internal MappingDbContext _mappingDbContext;
        protected DbSet<T> _dbSet;

        public GenericRepository(MappingDbContext mappingDbContext)
        {
            _mappingDbContext = mappingDbContext;
            _dbSet = _mappingDbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            EntityEntry user = await _dbSet.AddAsync(entity);
            await _mappingDbContext.SaveChangesAsync();
            return (T)user.Entity;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var user = await _dbSet.FirstOrDefaultAsync(expression);
            if (user != null)
            {
                _dbSet.Remove(user);
                await _mappingDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
        {
            return expression == null ? _dbSet : _dbSet.Where(expression);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var user = _dbSet.Update(entity);
            await _mappingDbContext.SaveChangesAsync();
            return user.Entity;
        }
    }
}
