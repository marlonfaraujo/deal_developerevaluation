using FluentValidation;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.DeleteProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductRequest>
    {
        public DeleteProductValidator()
        {
            RuleFor(product => product.Id)
                .NotEmpty()
                .WithMessage("Id produto obrigatório"); ;
        }
    }
}
