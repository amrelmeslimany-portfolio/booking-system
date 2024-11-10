using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace api.Config.Utils.Common
{
    public record FindAllParams<T>
    {
        public FindAllParams(
            int? pageNumber,
            int? pageSize,
            Expression<Func<T, bool>>? expression = null
        )
        {
            Expression = expression;
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize =
                pageSize > 100 ? 100
                : pageSize <= 0 ? 20
                : pageSize;
        }

        public Expression<Func<T, bool>>? Expression { get; }
        public int? PageNumber { get; }
        public int? PageSize { get; }
    }

    public record PaginationQuery(int? PageNumber = 1, int? PageSize = 20);
}
