namespace Deal.DeveloperEvaluation.WebApi.UseCases.ListProduct
{
    public record ListProductResult
    {
        public IEnumerable<ListProductResultData> Items { get; set; } = new List<ListProductResultData>();
        public long TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
    public record ListProductResultData
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
