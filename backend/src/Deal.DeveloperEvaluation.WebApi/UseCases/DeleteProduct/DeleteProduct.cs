using Deal.DeveloperEvaluation.WebApi.Repositories;
using FluentValidation;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.DeleteProduct
{
    public class DeleteProduct
    {
        private readonly IProductRepository _repository;

        public DeleteProduct(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task ExecuteAsync(DeleteProductRequest request, CancellationToken cancellationToken = default)
        {
            var validator = new DeleteProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existing = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (existing == null)
                throw new InvalidOperationException($"Product ID not found");

            await _repository.DeleteAsync(existing.Id, cancellationToken);
        }
        
    }
}
