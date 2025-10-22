using Deal.DeveloperEvaluation.WebApi.Entities;
using Deal.DeveloperEvaluation.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public class ProductRepository : IProductRepository
    {
        private readonly DefaultContext _context;
        public ProductRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            var result = await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task<IEnumerable<Product>> GetAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Product?> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            var local = _context.Set<Product>()
                .Local
                .FirstOrDefault(entry => entry.Id == product.Id);

            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return product;
        }
        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(new object[] { id }, cancellationToken);
            if (product == null)
                return false;

            _context.Products.Remove(product);

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
