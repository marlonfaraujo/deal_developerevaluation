using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Deal.DeveloperEvaluation.WebApi.Repositories;
using Deal.DeveloperEvaluation.WebApi.UseCases.CreateProduct;
using FluentValidation;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.GetProductById
{
    public class GetProductById
    {
        private readonly IProductRepository _repository;

        public GetProductById(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetProductByIdResult> ExecuteAsync(GetProductByIdRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new GetProductByIdValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found with id: " + request.Id.ToString());
            }
            return new GetProductByIdResult(
                    product.Id,
                    product.Name.Value,
                    product.Code.Value,
                    product.Price.Value
                );
        }
    }
}
