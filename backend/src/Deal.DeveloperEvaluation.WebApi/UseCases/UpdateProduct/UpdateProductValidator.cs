using FluentValidation;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(product => product.Id)
                .NotEmpty()
                .WithMessage("Product id is required.");

            RuleFor(product => product.Name)
                .NotEmpty()
                .WithMessage("Product name is required.");

            RuleFor(product => product.Price)
                .NotEmpty()
                .WithMessage("Product price is required.")
                .GreaterThan(0)
                .WithMessage("Product price must be greater than zero.");
        }
    }
}
