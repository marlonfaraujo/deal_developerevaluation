using FluentValidation;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("Nome do produto é obrigatório.");

            RuleFor(product => product.Code)
                .NotEmpty()
                .WithMessage("Código do produto é obrigatório.");

            RuleFor(product => product.Price)
                .NotEmpty()
                .WithMessage("Preço do produto é obrigatório.")
                .GreaterThan(0)
                .WithMessage("Preço do produto menor que zero.");
        }
    }
}
