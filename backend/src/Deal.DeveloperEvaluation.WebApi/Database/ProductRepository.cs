using Deal.DeveloperEvaluation.WebApi.Dtos;
using Deal.DeveloperEvaluation.WebApi.Entities;
using Deal.DeveloperEvaluation.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

        public async Task<PagedResult<Product>> GetAsync(QueryOptions options, CancellationToken cancellationToken = default)
        {
            var query = _context.Products
                .AsNoTracking()
                .AsQueryable();

            if (options.Filters != null && options.Filters.Any())
            {
                foreach (var kv in options.Filters)
                {
                    var property = typeof(Product).GetProperty(kv.Key);
                    if (property != null)
                    {
                        var value = kv.Value;
                        if (property.PropertyType == typeof(Guid) && value != null)
                        {
                            var guidValue = Guid.Parse(value.ToString()!);
                            query = query.Where(x => EF.Property<Guid>(x, kv.Key) == guidValue);
                        }
                        else
                        {
                            query = query.Where(x => EF.Property<string>(x, kv.Key) == value!.ToString());
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(options.SortBy))
            {
                var property = typeof(Product).GetProperty(options.SortBy);
                if (property != null)
                {
                    if (options.SortDescending)
                        query = query.OrderByDescending(x => EF.Property<object>(x, options.SortBy));
                    else
                        query = query.OrderBy(x => EF.Property<object>(x, options.SortBy));
                }
            }

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((options.Page - 1) * options.PageSize)
                .Take(options.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount,
                Page = options.Page,
                PageSize = options.PageSize
            };
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
