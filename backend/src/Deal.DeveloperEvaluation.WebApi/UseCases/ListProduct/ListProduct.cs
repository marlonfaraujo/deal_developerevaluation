using Deal.DeveloperEvaluation.WebApi.Repositories;

namespace Deal.DeveloperEvaluation.WebApi.UseCases.ListProduct
{
    public class ListProduct
    {
        private readonly IProductRepository _repository;

        public ListProduct(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ListProductResult>> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var products = await _repository.GetAsync(cancellationToken);
            return products.Select(product => new ListProductResult(
                product.Id,
                product.Name.Value,
                product.Code.Value,
                product.Price.Value
            ));
        }
    } 
}
