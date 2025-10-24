using Deal.DeveloperEvaluation.WebApi.Repositories;
using FluentValidation;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.UpdateProduct
{
    public class UpdateProduct
    {
        private readonly IProductRepository _repository;

        public UpdateProduct(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateProductResult> ExecuteAsync(UpdateProductRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new UpdateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (existing == null)
                throw new InvalidOperationException($"Id do produto não encontrado");

            existing.ChangeName(request.Name);
            existing.ChangePrice(request.Price);

            var updated = await _repository.UpdateAsync(existing, cancellationToken);
            return new UpdateProductResult(updated.Id, updated.Name.Value, updated.Code.Value, updated.Price.Value);
        }
    }
}
