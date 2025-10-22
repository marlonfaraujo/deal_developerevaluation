using Deal.DeveloperEvaluation.WebApi.Dtos;
using Deal.DeveloperEvaluation.WebApi.Entities;

namespace Deal.DeveloperEvaluation.WebApi.Repositories
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedResult<Product>> GetAsync(QueryOptions options, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
