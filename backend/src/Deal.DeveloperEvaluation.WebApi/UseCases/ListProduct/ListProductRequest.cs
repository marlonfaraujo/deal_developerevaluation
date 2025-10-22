namespace Deal.DeveloperEvaluation.WebApi.UseCases.ListProduct
{
    public record ListProductRequest
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string? SortBy { get; set; } = string.Empty;
        public string? SortDirection { get; set; } = string.Empty;
    }
}
