using Deal.DeveloperEvaluation.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public class ProductGuidFilterStrategy : IProductFilterStrategy
    {
        public bool CanHandle(Type propertyType)
        {
            return propertyType == typeof(Guid);
        }

        public IQueryable<Product> ApplyFilter(IQueryable<Product> query, string propertyName, object value)
        {
            var guidValue = Guid.Parse(value.ToString()!);
            return query.Where(x => EF.Property<Guid>(x, propertyName) == guidValue);
        }
    }
}