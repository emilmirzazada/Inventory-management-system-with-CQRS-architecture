using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sintra.Application.Interfaces;
using Sintra.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Sintra.Infrastructure.Persistence.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly IdentityContext _dbContext;
        private readonly IMapper mapper;

        public GenericRepositoryAsync(IdentityContext dbContext)
        {
            _dbContext = dbContext;
        }
        public GenericRepositoryAsync(IdentityContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            this.mapper = mapper;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task MakeDeleted(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }


        #region new

        public Task<List<TDest>> GetAsync<TEntity, TDest>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            try
            {
                IQueryable<TEntity> query = _dbContext.Set<TEntity>();
                
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (includes != null && includes.Length != 0)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }


                if (orderBy != null)
                {
                    return Task.FromResult(mapper.Map<IEnumerable<TDest>>(orderBy(query).AsEnumerable()).AsList());
                }
                else
                {
                    return Task.FromResult(mapper.Map<IEnumerable<TDest>>(query.ToList()).AsList());
                }
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                throw;
            }
        }

        public TDest GetById<TEntity, TDest>(object id) where TEntity : class
        {
            return mapper.Map<TDest>(_dbContext.Set<TEntity>().Find(id));
        }
        public async Task<TDest> GetByIdAsync<TEntity, TDest>(object id) where TEntity : class
        {
            return mapper.Map<TDest>(await _dbContext.Set<TEntity>().FindAsync(id));
        }

        public async Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public List<TDest> Get<TEntity, TDest>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") where TEntity : class
        {
            try
            {
                IQueryable<TEntity> query = _dbContext.Set<TEntity>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }


                if (orderBy != null)
                {
                    return mapper.Map<List<TDest>>(orderBy(query).ToList());
                }
                else
                {
                    return mapper.Map<List<TDest>>(query.ToList());
                }
            }
            catch (Exception ex)
            {
                var res = ex.Message;
                throw;
            }
        }

        public async Task<List<TDest>> GetAsync<TEntity, TDest>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") where TEntity : class
        {
            DbSet<TEntity> dbSet = _dbContext.Set<TEntity>();
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return mapper.Map<List<TDest>>(await orderBy(query).AsNoTracking().ToListAsync());
            }
            else
            {
                return mapper.Map<List<TDest>>(await query.AsNoTracking().ToListAsync());
            }
        }

        public async Task<List<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") where TEntity : class
        {
            DbSet<TEntity> dbSet = _dbContext.Set<TEntity>();
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).AsNoTracking().ToListAsync();
            }
            else
            {
                return await query.AsNoTracking().ToListAsync();
            }
        }

        public TEntity Insert<TEntity, TDest>(TDest dest, dynamic obj = null) where TEntity : class
        {

            TEntity entity = mapper.Map<TEntity>(dest);

            if (obj != null)
            {
                var t = typeof(TEntity);

                var str = obj.GetType().GetProperties();

                foreach (var k in str)
                {
                    dynamic prop = t.GetProperty(k.Name);
                    dynamic value = obj.GetType().GetProperty(k.Name).GetValue(obj, null);
                    prop.SetValue(entity, value);
                }
            }

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void InsertRange<TEntity, TDest>(List<TDest> dest) where TEntity : class
        {
            List<TEntity> entity = mapper.Map<List<TEntity>>(dest);
            _dbContext.AddRange(entity);
            _dbContext.SaveChanges();
        }

        public TEntity Update<TEntity, TDest>(TDest dest, string ignores = null, dynamic obj = null) where TEntity : class
        {
            TEntity newEntity = mapper.Map<TEntity>(dest);

            if (ignores != null)
            {
                TEntity oldEntity = _dbContext.Set<TEntity>().Find(newEntity.GetType().GetProperty("Id").GetValue(newEntity, null));


                var t = typeof(TEntity);

                if (obj != null)
                {
                    var str = obj.GetType().GetProperties();

                    foreach (var k in str)
                    {
                        dynamic prop = t.GetProperty(k.Name);
                        dynamic value = obj.GetType().GetProperty(k.Name).GetValue(obj, null);
                        prop.SetValue(newEntity, value);
                    }
                }

                foreach (var ignore in ignores.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    dynamic prop = t.GetProperty(ignore);
                    dynamic value = oldEntity.GetType().GetProperty(ignore).GetValue(oldEntity);
                    prop.SetValue(newEntity, value);
                }

                _dbContext.Entry(oldEntity).State = EntityState.Detached;
            }

            _dbContext.Update(newEntity);
            _dbContext.SaveChanges();
            return newEntity;
        }

        public void UpdateRange<TEntity, TDest>(List<TDest> dest) where TEntity : class
        {
            List<TEntity> entityToUpdate = mapper.Map<List<TEntity>>(dest);
            _dbContext.UpdateRange(entityToUpdate);
            _dbContext.SaveChanges();
        }

        public void Delete<TEntity>(object id) where TEntity : class
        {
            TEntity entityToDelete = _dbContext.Set<TEntity>().Find(id);
            _dbContext.Remove(entityToDelete);
            _dbContext.SaveChanges();
        }

        public void DeleteRange<TEntity, TDest>(List<TDest> dests) where TEntity : class
        {
            List<TEntity> entities = mapper.Map<List<TEntity>>(dests);
            _dbContext.RemoveRange(entities);
            _dbContext.SaveChanges();
        }

        public void DeleteRangeWithFilter<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            List<TEntity> entityToDelete = query.ToList();
            _dbContext.RemoveRange(entityToDelete);
            _dbContext.SaveChanges();
        }

        public TEntity UpdateDeleted<TEntity>(object id, dynamic obj = null) where TEntity : class
        {
            TEntity newEntity = _dbContext.Set<TEntity>().Find(id);

            var t = typeof(TEntity);

            if (obj != null)
            {
                var str = obj.GetType().GetProperties();

                foreach (var k in str)
                {
                    dynamic prop = t.GetProperty(k.Name);
                    dynamic value = obj.GetType().GetProperty(k.Name).GetValue(obj, null);
                    prop.SetValue(newEntity, value);
                }
            }
            _dbContext.Entry(newEntity).State = EntityState.Detached;

            _dbContext.Update(newEntity);
            _dbContext.SaveChanges();
            return newEntity;
        }

        #endregion
    }
}
