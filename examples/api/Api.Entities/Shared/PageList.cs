using System.Text.Json;

namespace Api.Entities
{
    public class PageList<T> : List<T>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalRecords { get; private set; }
        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        public string Meta
        {
            get
            {
                var metadata = new
                {
                    pageNumber = PageNumber,
                    pageSize = PageSize,
                    totalPages = TotalPages,
                    totalRecords = TotalRecords,
                    hasPrevious = HasPrevious,
                    hasNext = HasNext
                };

                return JsonSerializer.Serialize(metadata);
            }
        }

        public PageList(List<T> items, int count, int pageNumber, int pageSize) : 
            base (items)
        {
            TotalRecords = count;
            PageSize = pageSize;
            PageNumber = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double)pageSize);
        }

        public static PageList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}
