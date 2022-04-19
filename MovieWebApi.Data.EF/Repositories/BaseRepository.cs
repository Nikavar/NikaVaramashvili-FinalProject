using Microsoft.EntityFrameworkCore;
using MovieWebApi.PersistenceDB.Context;
using PersistenceDb.MVC.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace MovieWebApi.Data.EF
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        #region Protected
        protected readonly MVCDbContext _context;
        protected readonly DbSet<T> _dbSet;
        #endregion

        #region Ctor
        public BaseRepository(MVCDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        #endregion

        #region Property
        public IQueryable<T> Table
        {
            get { return _dbSet; }
        }

        #endregion

        #region Methods

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync(params object[] key)
        {
            return await _dbSet.FindAsync(key);
        }

        public async Task<T> GetAsync(string expresion, params object[] key)
        {
            return await _dbSet.FindAsync(key);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                return;

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(params object[] key)
        {
            var entity = await GetAsync(key);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AnyAsync(predicate);
        }

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] property)
        {
            IQueryable<T> query = _dbSet;
            for (int i = 0; i < property.Length; i++)
            {
                query = property.Aggregate(query, (x, property) => x.Include(property));
            }

            return await query.ToListAsync();
        }

        #endregion

    }
}
