namespace Deal.DeveloperEvaluation.WebApi.UseCases.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, decimal Price)
    {
    }
}
