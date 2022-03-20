using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sintra.Application.Interfaces
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task MakeDeleted(T entity);

        #region new
        public TEntity Insert<TEntity, TDest>(TDest dest, dynamic obj = null) where TEntity : class;
        public void InsertRange<TEntity, TDest>(List<TDest> dest) where TEntity : class;
        public void UpdateRange<TEntity, TDest>(List<TDest> dest) where TEntity : class;
        public TEntity Update<TEntity, TDest>(TDest dest, string ignores = null, dynamic obj = null) where TEntity : class;
        public void Delete<TEntity>(object id) where TEntity : class;
        public void DeleteRange<TEntity, TDest>(List<TDest> dests) where TEntity : class;
        public List<TDest> Get<TEntity, TDest>(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "") where TEntity : class;
        public Task<List<TDest>> GetAsync<TEntity, TDest>(Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;
        public Task<List<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;
        Task<List<TDest>> GetAsync<TEntity, TDest>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes) where TEntity : class;
        public TDest GetById<TEntity, TDest>(object id) where TEntity : class;
        public Task<TDest> GetByIdAsync<TEntity, TDest>(object id) where TEntity : class;
        public Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class;
        public TEntity UpdateDeleted<TEntity>(object id, dynamic obj = null) where TEntity : class;
        #endregion
    }
}
