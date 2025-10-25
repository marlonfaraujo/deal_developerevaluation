using Deal.DeveloperEvaluation.WebApi.Dtos;
using Deal.DeveloperEvaluation.WebApi.Entities;
using Deal.DeveloperEvaluation.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                var filterStrategyExecutor = new ProductFilterStrategyExecutor();
                foreach (var kv in options.Filters)
                {
                    var property = typeof(Product).GetProperty(kv.Key);
                    if (property != null && kv.Value != null)
                    {
                        filterStrategyExecutor.SetFilterStrategy(ProductFilterStrategyFactory.Create(property.PropertyType));
                        query = filterStrategyExecutor.ExecuteFilter(query, kv.Key, kv.Value);
                    }
                }
            }

            query = GetOrderedBy(query, options);
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

        private IQueryable<Product> GetOrderedBy(IQueryable<Product> query, QueryOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.SortBy))
            {
                return query;
            }
            var propertyInfo = typeof(Product).GetProperty(options.SortBy);
            if (propertyInfo == null)
            {
                return query;
            }

            var valueObjectMap = new Dictionary<string, Expression<Func<Product, object>>>(StringComparer.OrdinalIgnoreCase)
            {
                { "Name", p => p.Name.Value },
                { "Code", p => p.Code.Value },
                { "Price", p => p.Price.Value }
            };

            if (valueObjectMap.TryGetValue(options.SortBy, out var keySelector))
            {
                return options.SortDescending
                    ? query.OrderByDescending(keySelector)
                    : query.OrderBy(keySelector);
            }

            return options.SortDescending
                ? query.OrderByDescending(p => EF.Property<object>(p, propertyInfo.Name))
                : query.OrderBy(p => EF.Property<object>(p, propertyInfo.Name));
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
