using Deal.DeveloperEvaluation.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public class ProductNameFilterStrategy : IProductFilterStrategy
    {
        public bool CanHandle(Type propertyType)
        {
            return propertyType == typeof(ProductName);
        }
        public IQueryable<Product> ApplyFilter(IQueryable<Product> query, string propertyName, object value)
        {
            var nameValue = new ProductName(value.ToString()!);
            return query.Where(x => EF.Property<ProductName>(x, propertyName).Value == nameValue.Value);
        }
    }
}
