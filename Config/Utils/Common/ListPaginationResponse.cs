namespace api.Config.Utils.Common
{
    public record ListPaginationResponse<T>(
        List<T> Data,
        int DataSize,
        int PageNumber,
        int PageSize
    )
    {
        public bool HasNext => PageNumber * PageSize < DataSize;
        public bool HasPrevious => PageNumber > 1;
    };
}
