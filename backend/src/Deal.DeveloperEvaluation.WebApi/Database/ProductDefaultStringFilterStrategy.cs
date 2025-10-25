using Deal.DeveloperEvaluation.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public class ProductDefaultStringFilterStrategy : IProductFilterStrategy
    {
        public bool CanHandle(Type propertyType)
        {
            return propertyType == typeof(string);
        }
        public IQueryable<Product> ApplyFilter(IQueryable<Product> query, string propertyName, object value)
        {
            return query.Where(x => EF.Property<string>(x, propertyName) == value!.ToString());
        }
    }
}