using System.Linq.Expressions;
using Ecommerce.Entities.Abstractions;

namespace Ecommerce.DAL.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity: class, IEntityBase<TKey>
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Detach(TEntity entity);

        void DetachRange(IEnumerable<TEntity> entities);

        Task<TEntity> GetOneAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);
        
        Task<TEntity> GetOneAsync(TKey id, CancellationToken token, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetOneWhereAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        
        Task<TEntity> GetOneWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token, params Expression<Func<TEntity, object>>[] includes);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> GetAllWhere(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task RemoveAsync(TKey id);
        
        Task RemoveAsync(TEntity entity);

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entities);

        void MarkDeleted(TEntity entity);

        void MarkDeletedRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entities);

        IDisposable BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
        
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}