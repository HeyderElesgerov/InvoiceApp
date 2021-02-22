using InvoiceApp.Domain.Core.Entity;
using InvoiceApp.Domain.Core.RepositoryBase;
using InvoiceApp.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvoiceApp.Infrastructure.Repository
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        protected ApplicationDbContext Db;
        protected DbSet<TEntity> Entities;

        public Repository(ApplicationDbContext db)
        {
            Db = db;
            Entities = db.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await Entities.AddAsync(entity);
        }

        public async Task AddRange(ICollection<TEntity> list)
        {
            await Entities.AddRangeAsync(list);
        }

        public async Task<bool> All(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.AllAsync(predicate);
        }

        public async Task<bool> Any(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.AnyAsync(predicate);
        }

        public async Task Commit()
        {
            await Db.SaveChangesAsync();
        }

        public async Task<int> Count()
        {
            return await Entities.CountAsync();
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.CountAsync(predicate);
        }

        public Task Delete(TEntity entity)
        {
            return Task.Run(() => Entities.Remove(entity));
        }

        public Task DeleteWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() => Entities.RemoveRange(Entities.Where(predicate)));
        }

        public Task<bool> Exists(TKey primaryKey)
        {
            return Task.FromResult(Find(primaryKey) != null);
        }

        public TEntity Find(TKey id)
        {
            return Entities.Find(id);
        }

        public Task<IEnumerable<TEntity>> GetAll()
        {
            return Task.Run(() => Entities.AsEnumerable());
        }

        public Task<IEnumerable<TEntity>> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            if (includeProperties == null)
                return Task.Run(() => Entities.AsEnumerable());

            var entitiesAsQueryable = Entities.AsQueryable();

            foreach (var includeProp in includeProperties)
            {
                entitiesAsQueryable = entitiesAsQueryable.Include(includeProp);
            }

            return Task.Run(() => entitiesAsQueryable.AsEnumerable());
        }

        public async Task<TEntity> GetFirst(TKey id)
        {
            return await Entities.FirstAsync(t => t.Id.Equals(id));
        }

        public async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return await Entities.FirstAsync(predicate);
        }

        public async Task<TEntity> GetFirstIncluding(TKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entitiesAsQueryable = Entities.AsQueryable();

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties)
                {
                    entitiesAsQueryable = entitiesAsQueryable.Include(includeProp);
                }
            }

            return await entitiesAsQueryable.FirstAsync(t => t.Id.Equals(id));
        }

        public async Task<TEntity> GetFirstIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entitiesAsQueryable = Entities.AsQueryable();

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties)
                {
                    entitiesAsQueryable = entitiesAsQueryable.Include(includeProp);
                }
            }

            return await entitiesAsQueryable.FirstAsync(predicate);
        }

        public Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.Run(() => Entities.Where(predicate).AsEnumerable());
        }

        public Task<IEnumerable<TEntity>> GetWhereIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = Entities.Where(predicate);

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties)
                {
                    entities = entities.Include(includeProp);
                }
            }

            return Task.Run(() => entities.AsEnumerable());
        }

        public Task Update(TEntity entity)
        {
            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
