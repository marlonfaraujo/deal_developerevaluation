using Bogus;
using Deal.DeveloperEvaluation.WebApi.Entities;

namespace Deal.DeveloperEvaluation.Integration.TestData
{
    public static class ProductFaker
    {
        public static List<Product> GenerateFakeProducts(int count = 10)
        {
            var faker = new Faker<Product>()
                .CustomInstantiator(f => new Product(
                    f.Commerce.ProductName(),
                    f.Commerce.Ean13(), 
                    decimal.Parse(f.Commerce.Price(10, 200))
                ));

            return faker.Generate(count);
        }
    }
}
