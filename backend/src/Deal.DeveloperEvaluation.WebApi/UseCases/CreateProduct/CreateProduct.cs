using Deal.DeveloperEvaluation.WebApi.Dtos;
using Deal.DeveloperEvaluation.WebApi.Entities;
using Deal.DeveloperEvaluation.WebApi.Repositories;
using FluentValidation;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.CreateProduct
{
    public class CreateProduct
    {
        private readonly IProductRepository _repository;

        public CreateProduct(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateProductResult> ExecuteAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new CreateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var queryOptions = new QueryOptions()
            {
                Filters = new Dictionary<string, object>() { { "Code", request.Code } }
            };
            var existsProductCode = await _repository.GetAsync(queryOptions, cancellationToken);
            if (existsProductCode.Items.Any())
                throw new ValidationException($"Product with code '{request.Code}' already exists.");

            var product = new Product(request.Name, request.Code, request.Price);
            var result = await _repository.AddAsync(product);
            return new CreateProductResult(result.Id, result.Name.Value, result.Code.Value, result.Price.Value);
        }
    }
}
