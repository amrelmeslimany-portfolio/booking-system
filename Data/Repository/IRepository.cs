using System.Linq.Expressions;
using api.Config.Utils.Common;

namespace api.Data.Repository
{
    public interface IRepository<T>
        where T : class
    {
        Task<T?> FindById(Guid id, bool? isTrack = false);
        Task<T?> FindWhere(Expression<Func<T?, bool>> predicate);
        IQueryable<T> FindAll(FindAllParams<T> findAllParams);
        Task<int> FindSize(Expression<Func<T, bool>>? predicate = null);
        Task<T?> Create(T model);
        Task Update(T model);
        Task Delete(T model);
    }
}
