using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections;
using System.Linq.Expressions;

namespace EFCore.DbSetPattern.Context
{
    public abstract class DbSetProxy<TEntity> : IQueryable<TEntity> where TEntity : class
    {
        protected readonly DbSet<TEntity> DbSet;

        public IEntityType EntityType => DbSet.EntityType;
        public LocalView<TEntity> Local => DbSet.Local;
        public Type ElementType => ((IQueryable)DbSet).ElementType;
        public Expression Expression => ((IQueryable)DbSet).Expression;
        public IQueryProvider Provider => ((IQueryable)DbSet).Provider;

        public DbSetProxy(DbSet<TEntity> dbSet)
        {
            DbSet = dbSet;
        }

        public EntityEntry<TEntity> Add(TEntity entity)
        {
            return DbSet.Add(entity);
        }

        public ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            return DbSet.AddAsync(entity, cancellationToken);
        }

        public void AddRange(params TEntity[] entities)
        {
            DbSet.AddRange(entities);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public Task AddRangeAsync(params TEntity[] entities)
        {
            return DbSet.AddRangeAsync(entities);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            return DbSet.AddRangeAsync(entities, cancellationToken);
        }

        public IAsyncEnumerable<TEntity> AsAsyncEnumerable()
        {
            return DbSet.AsAsyncEnumerable();
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return DbSet.AsQueryable();
        }

        public EntityEntry<TEntity> Attach(TEntity entity)
        {
            return DbSet.Attach(entity);
        }

        public void AttachRange(params TEntity[] entities)
        {
            DbSet.AttachRange(entities);
        }

        public void AttachRange(IEnumerable<TEntity> entities)
        {
            DbSet.AttachRange(entities);
        }

        public EntityEntry<TEntity> Entry(TEntity entity)
        {
            return DbSet.Entry(entity);
        }

        public bool Equals(object? obj)
        {
            return DbSet.Equals(obj);
        }

        public TEntity? Find(params object?[]? keyValues)
        {
            return DbSet.Find(keyValues);
        }

        public ValueTask<TEntity?> FindAsync(params object?[]? keyValues)
        {
            return DbSet.FindAsync(keyValues);
        }

        public ValueTask<TEntity?> FindAsync(object?[]? keyValues, CancellationToken cancellationToken)
        {
            return DbSet.FindAsync(keyValues, cancellationToken);
        }

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return DbSet.GetAsyncEnumerator(cancellationToken);
        }

        public int GetHashCode()
        {
            return DbSet.GetHashCode();
        }

        public EntityEntry<TEntity> Remove(TEntity entity)
        {
            return DbSet.Remove(entity);
        }

        public void RemoveRange(params TEntity[] entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public string? ToString()
        {
            return DbSet.ToString();
        }

        public EntityEntry<TEntity> Update(TEntity entity)
        {
            return DbSet.Update(entity);
        }

        public void UpdateRange(params TEntity[] entities)
        {
            DbSet.UpdateRange(entities);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return ((IEnumerable<TEntity>)DbSet).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)DbSet).GetEnumerator();
        }
    }
}
