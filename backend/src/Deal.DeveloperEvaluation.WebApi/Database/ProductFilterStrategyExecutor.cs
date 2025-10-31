using Deal.DeveloperEvaluation.WebApi.Entities;

namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public class ProductFilterStrategyExecutor
    {
        public IProductFilterStrategy FilterStrategy { get; private set; }

        public void SetFilterStrategy(IProductFilterStrategy newStrategy)
        {
            FilterStrategy = newStrategy;
        }

        public IQueryable<Product> ExecuteFilter(IQueryable<Product> query, string propertyName, object value)
        {
            return FilterStrategy.ApplyFilter(query, propertyName, value);
        }
    }
}
