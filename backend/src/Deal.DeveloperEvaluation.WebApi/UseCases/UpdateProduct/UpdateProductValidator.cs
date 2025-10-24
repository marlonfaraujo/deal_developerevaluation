using FluentValidation;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(product => product.Id)
                .NotEmpty()
                .WithMessage("Id do produto é obrigatório.");

            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("Nome do produto é obrigatório.");

            RuleFor(product => product.Price)
                .NotEmpty()
                .WithMessage("Preço do produto é obrigatório.")
                .GreaterThan(0)
                .WithMessage("Preço do produto menor que zero.");
        }
    }
}
