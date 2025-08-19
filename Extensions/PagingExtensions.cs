using ProvaPub.Results;

namespace ProvaPub.Extensions
{
    public static class PagingExtensions
    {
        public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> source, int page, int pageSize = 10)
        {
            var totalItems = source.Count();
            var items = source
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<T>
            {
                HasNext = page * pageSize < totalItems,
                TotalCount = totalItems,
                Items = items
            };
        }
    }
}
