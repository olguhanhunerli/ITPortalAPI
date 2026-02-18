using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ITPortal.Business.Repository.GenericRepository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
       where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _set;
        protected readonly IHttpContextAccessor _http;

        public GenericRepository(DbContext context, IHttpContextAccessor http)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<TEntity>();
            _http = http;
        }

        public IQueryable<TEntity> Query(bool asNoTracking = true)
            => asNoTracking ? _set.AsNoTracking() : _set;

        public async Task<TEntity?> GetByIdAsync(TKey id, bool asNoTracking = true, CancellationToken ct = default)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var entity = await _set.FindAsync(new object?[] { id }, ct);

            if (entity == null) return null;

            if (asNoTracking)
                _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<List<TEntity>> GetAllAsync(
            bool asNoTracking = true,
            CancellationToken ct = default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var q = BuildQuery(asNoTracking, includes);
            return await q.ToListAsync(ct);
        }

        public async Task<List<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool asNoTracking = true,
            CancellationToken ct = default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            var q = BuildQuery(asNoTracking, includes);
            return await q.Where(predicate).ToListAsync(ct);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            bool asNoTracking = true,
            CancellationToken ct = default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            var q = BuildQuery(asNoTracking, includes);
            return await q.FirstOrDefaultAsync(predicate, ct);
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return _set.AnyAsync(predicate, ct);
        }

        public Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken ct = default)
            => predicate == null ? _set.CountAsync(ct) : _set.CountAsync(predicate, ct);

        public async Task<(List<TEntity> Items, int TotalCount)> GetPagedAsync(
            Expression<Func<TEntity, bool>>? predicate,
            int pageNumber,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            bool asNoTracking = true,
            CancellationToken ct = default,
            params Expression<Func<TEntity, object>>[] includes)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;

            var q = BuildQuery(asNoTracking, includes);

            if (predicate != null)
                q = q.Where(predicate);

            var total = await q.CountAsync(ct);

            if (orderBy != null)
                q = orderBy(q);

            var items = await q.Skip((pageNumber - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync(ct);

            return (items, total);
        }

        public Task AddAsync(TEntity entity, CancellationToken ct = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return _set.AddAsync(entity, ct).AsTask();
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct = default)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return _set.AddRangeAsync(entities, ct);
        }

        public void Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _set.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            if (TrySoftDelete(entity))
            {
                _set.Update(entity);
                return;
            }

            _set.Remove(entity);
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _context.SaveChangesAsync(ct);

        private IQueryable<TEntity> BuildQuery(bool asNoTracking, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> q = asNoTracking ? _set.AsNoTracking() : _set;

            if (includes != null)
            {
                foreach (var inc in includes)
                    if (inc != null) q = q.Include(inc);
            }

            return q;
        }

        private static bool TrySoftDelete(TEntity entity)
        {
            var prop = typeof(TEntity).GetProperty("DeletedAt",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

            if (prop == null) return false;

            if (prop.PropertyType == typeof(DateTime?))
            {
                prop.SetValue(entity, DateTime.UtcNow);
                return true;
            }

            if (prop.PropertyType == typeof(DateTime))
            {
                prop.SetValue(entity, DateTime.UtcNow);
                return true;
            }

            return false;
        }
    }
}