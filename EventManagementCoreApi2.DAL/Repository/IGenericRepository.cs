using EventManagementCoreApi2.DAL.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementCoreApi2.DAL.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetUserInfoAsync(Expression<Func<TEntity, bool>> condition);
        Task<bool> AddAsync(TEntity obj);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> UpdateAsync(TEntity obj);
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private DatabaseContext _dbContext;
        private DbSet<T> _dbSet;

        public GenericRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> GetUserInfoAsync(Expression<Func<T, bool>> condition)
        {
            var userInfo = await _dbSet.Where(condition).SingleOrDefaultAsync();

            return userInfo;
        }

        public async Task<bool> AddAsync(T obj)
        {
            try
            {
                _dbContext.Set<T>().Add(obj);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
