using FluentValidation;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("Product name is required.");

            RuleFor(product => product.Code)
                .NotEmpty()
                .WithMessage("Product code is required.");

            RuleFor(product => product.Price)
                .NotEmpty()
                .WithMessage("Product price is required.")
                .GreaterThan(0)
                .WithMessage("Product price must be greater than zero.");
        }
    }
}
