using Deal.DeveloperEvaluation.WebApi.Entities;

namespace Deal.DeveloperEvaluation.WebApi.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product proposal, CancellationToken cancellationToken = default);
        Task<Product?> UpdateAsync(Product proposal, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetAsync(CancellationToken cancellationToken = default);
    }
}
