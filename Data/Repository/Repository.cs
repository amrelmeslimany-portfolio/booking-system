using System.Linq.Expressions;
using api.Config.Utils.Common;
using api.Models.Common;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Repository
{
    public class Repository<T> : IRepository<T>
        where T : BaseModel
    {
        private readonly DataAppContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DataAppContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> Create(T model)
        {
            await _dbSet.AddAsync(model);
            return model;
        }

        public Task Delete(T model)
        {
            _dbSet.Remove(model);
            return Task.CompletedTask;
        }

        public IQueryable<T> FindAll(FindAllParams<T> findAllParams, string? includeProps)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (findAllParams.Expression != null)
                query = query.Where(findAllParams.Expression);

            if (includeProps != null)
            {
                query = includeProps
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, item) => current.Include(item));
            }

            return query
                .Skip((int)(findAllParams.PageNumber - 1)! * (int)findAllParams.PageSize!)
                .Take((int)findAllParams.PageSize);
        }

        public async Task<T?> FindById(Guid id, string? includeProps = null)
        {
            IQueryable<T> query = _dbSet.Where(item => item.Id == id);

            if (includeProps != null)
            {
                query = includeProps
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Aggregate(query, (current, item) => current.Include(item));
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<int> FindSize(Expression<Func<T, bool>>? predicate)
        {
            IQueryable<T> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync();
        }

        public async Task<T?> FindWhere(Expression<Func<T?, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public Task Update(T model)
        {
            _dbSet.Update(model);
            return Task.CompletedTask;
        }
    }
}
