namespace Deal.DeveloperEvaluation.WebApi.UseCases.ListProduct
{
    public record ListProductResult(Guid Id, string Name, string Code, decimal Price)
    {
    }
}
