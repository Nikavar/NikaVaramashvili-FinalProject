using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieWebApi.Data
{
    public interface IBaseRepository<T> where T : class
    {
        #region Methods
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] property);
        Task<T> GetAsync(params object[] key);
        Task AddAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveAsync(params object[] key);
        Task UpdateAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T,bool>> predicate);
        #endregion

        #region Sets
        IQueryable<T> Table { get; }

        #endregion
    }
}
