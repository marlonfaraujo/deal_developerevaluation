namespace Deal.DeveloperEvaluation.WebApi.Dtos
{
    public class QueryOptions
    {
        public int Page { get; set; } = 1;       
        public int PageSize { get; set; } = 10; 
        public string? SortBy { get; set; }      
        public bool SortDescending { get; set; } = false;
        public Dictionary<string, object>? Filters { get; set; }
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public long TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
