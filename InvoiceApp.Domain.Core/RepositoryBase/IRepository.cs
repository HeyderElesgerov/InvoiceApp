using InvoiceApp.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Core.RepositoryBase
{
    public interface IRepository<TEntity, in TPrimaryKey> where TEntity : BaseEntity<TPrimaryKey>
    {
        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetWhereIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        
        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetFirst(TPrimaryKey id);

        Task<TEntity> GetFirstIncluding(TPrimaryKey id, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetFirstIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity Find(TPrimaryKey id);

        Task<bool> Any(Expression<Func<TEntity, bool>> predicate);


        Task<bool> All(Expression<Func<TEntity, bool>> predicate);

        Task<int> Count();

        Task<int> Count(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);
        Task AddRange(ICollection<TEntity> list);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);

        Task DeleteWhere(Expression<Func<TEntity, bool>> predicate);

        Task<bool> Exists(TPrimaryKey primaryKey);

        Task Commit();
    }
}
