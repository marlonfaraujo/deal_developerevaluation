using Deal.DeveloperEvaluation.WebApi.Entities;

namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public interface IProductFilterStrategy
    {
        bool CanHandle(Type propertyType);
        IQueryable<Product> ApplyFilter(IQueryable<Product> query, string propertyName, object value);
    }
}
