using Deal.DeveloperEvaluation.WebApi.Dtos;
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

        public async Task<ListProductResult> ExecuteAsync(ListProductRequest request, CancellationToken cancellationToken = default)
        {
            var queryOptions = GetQueryOptions(request);
            var products = await _repository.GetAsync(queryOptions, cancellationToken);
            return new ListProductResult { 
                Items = products.Items.Select(product => new ListProductResultData
                {
                    Id = product.Id,
                    Name = product.Name.Value,
                    Code = product.Code.Value,
                    Price = product.Price.Value
                }),
                Page = products.Page,
                PageSize = products.PageSize,
                TotalCount = products.TotalCount
            };
        }

        private QueryOptions GetQueryOptions(ListProductRequest request)
        {
            var filters = new Dictionary<string, object>();
            var options = new QueryOptions();

            if (!string.IsNullOrWhiteSpace(request.Name))
                filters["Name"] = request.Name;

            if (!string.IsNullOrWhiteSpace(request.Code))
                filters["Code"] = request.Code;

            if (request.PageNumber.HasValue && request.PageNumber > 0)
            {
                options.Page = request.PageNumber.Value;
            }
            if (request.PageSize.HasValue && request.PageSize > 0)
            {
                options.PageSize = request.PageSize.Value;
            }
            if (!string.IsNullOrWhiteSpace(request.SortBy))
            {
                options.SortBy = request.SortBy;
                options.SortDescending = string.Equals(request.SortDirection, "desc", StringComparison.OrdinalIgnoreCase);
            }
            options.Filters = filters;

            return options;
        }
    } 
}
