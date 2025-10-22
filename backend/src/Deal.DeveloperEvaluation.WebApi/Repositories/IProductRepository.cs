using Deal.DeveloperEvaluation.WebApi.Entities;

namespace Deal.DeveloperEvaluation.WebApi.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetAsync(CancellationToken cancellationToken = default);
    }
}
