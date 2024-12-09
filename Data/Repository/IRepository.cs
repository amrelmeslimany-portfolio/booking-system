using System.Linq.Expressions;
using api.Config.Utils.Common;
using api.Models.Common;

namespace api.Data.Repository
{
    public interface IRepository<T>
        where T : BaseModel
    {
        Task<T?> FindById(Guid id, string? includeProps = null);
        Task<T?> FindWhere(Expression<Func<T?, bool>> predicate);
        IQueryable<T> FindAll(FindAllParams<T> findAllParams, string? includeProps = null);
        Task<int> FindSize(Expression<Func<T, bool>>? predicate = null);
        Task<T?> Create(T model);
        Task Update(T model);
        Task Delete(T model);
    }
}
