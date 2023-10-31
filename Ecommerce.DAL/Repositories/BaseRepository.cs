using System.Linq.Expressions;
using Ecommerce.Common.Utils;
using Ecommerce.DAL.Data;
using Ecommerce.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Ecommerce.DAL.Repositories
{
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> 
        where TEntity : class, IEntityBase<TKey>
    {
        protected EcommerceDbContext _dbContext { get; }
        protected IDbContextTransaction _dbTransaction;
        protected DbSet<TEntity> _dbSet;

        public BaseRepository(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Detach(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        public void DetachRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
        }

        public Task<TEntity> GetOneAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
        {
            return WithIncludes(_dbSet, includes).SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public Task<TEntity> GetOneAsync(TKey id, CancellationToken token, params Expression<Func<TEntity, object>>[] includes)
        {
            return WithIncludes(_dbSet, includes).SingleOrDefaultAsync(e => e.Id.Equals(id), token);
        }

        public Task<TEntity> GetOneWhereAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return WithIncludes(_dbSet, includes).SingleOrDefaultAsync(predicate);
        }

        public Task<TEntity> GetOneWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token, params Expression<Func<TEntity, object>>[] includes)
        {
            return WithIncludes(_dbSet, includes).SingleOrDefaultAsync(predicate, token);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> GetAllWhere(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return WithIncludes(_dbSet, includes).Where(predicate);
        }

        public async Task RemoveAsync(TKey id)
        {
            var entity = await GetOneAsync(id);
            Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
        
        public virtual Task RemoveAsync(TEntity entity)
        {
            return Task.Run(() => _dbSet.Remove(entity));
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public void MarkDeleted(TEntity entity)
        {
            var deletedTimeProperty = entity.GetType().GetProperty("DeletedTime");
            if (deletedTimeProperty is not null)
            {
                deletedTimeProperty.SetValue(entity, DateTime.UtcNow.RoundToSeconds());
            }
        }

        public void MarkDeletedRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                MarkDeleted(entity);
            }
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public IDisposable BeginTransaction()
        {
            return _dbTransaction = _dbContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _dbContext.Commit();
            _dbTransaction = null;
        }

        public void RollbackTransaction()
        {
            if (_dbTransaction is not null)
            {
                _dbContext.Rollback();
            }
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            return _dbContext.SaveChangesAsync(token);
        }
        
        protected IQueryable<TEntity> WithIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                    (current, include) => current.Include(include));
            }

            return query;
        }
    }
}