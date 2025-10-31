using Deal.DeveloperEvaluation.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public class ProductSkuFilterStrategy : IProductFilterStrategy
    {
        public bool CanHandle(Type propertyType)
        {
            return propertyType == typeof(Sku);
        }

        public IQueryable<Product> ApplyFilter(IQueryable<Product> query, string propertyName, object value)
        {
            var skuValue = new Sku(value.ToString()!);
            return query.Where(x => EF.Property<Sku>(x, propertyName).Value == skuValue.Value);
        }
    }
}