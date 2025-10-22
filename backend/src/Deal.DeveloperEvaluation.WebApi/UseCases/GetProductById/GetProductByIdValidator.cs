using Deal.DeveloperEvaluation.WebApi.UseCases.GetProductById;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdRequest>
    {
        public GetProductByIdValidator()
        {
            RuleFor(product => product.Id)
                .NotEmpty()
                .WithMessage("Product ID is required"); ;
        }
    }
}
