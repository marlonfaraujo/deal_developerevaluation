namespace Deal.DeveloperEvaluation.WebApi.UseCases.UpdateProduct
{
    public record UpdateProductResult(Guid Id, string Name, string Code, decimal Price)
    {
    }
}
