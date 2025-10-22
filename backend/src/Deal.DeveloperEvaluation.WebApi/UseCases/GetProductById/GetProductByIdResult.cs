namespace Deal.DeveloperEvaluation.WebApi.UseCases.GetProductById
{
    public record GetProductByIdResult(Guid Id, string Name, string Code, decimal Price)
    {
    }
}
