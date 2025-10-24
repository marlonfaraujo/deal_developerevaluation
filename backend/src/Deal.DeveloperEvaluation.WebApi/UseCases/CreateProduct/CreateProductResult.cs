namespace Deal.DeveloperEvaluation.WebApi.UseCases.CreateProduct
{
    public record CreateProductResult(Guid Id, string Name, string Code, decimal Price)
    {
    }
}
