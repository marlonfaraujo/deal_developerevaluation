using Bogus;
using Deal.DeveloperEvaluation.WebApi.Entities;
using Deal.DeveloperEvaluation.WebApi.UseCases.CreateProduct;
using Deal.DeveloperEvaluation.WebApi.UseCases.UpdateProduct;

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

        public static CreateProductRequest GenerateFakeCreateProductRequest()
        {
            var faker = new Faker<CreateProductRequest>()
                .CustomInstantiator(f => new CreateProductRequest(
                    f.Commerce.ProductName(),
                    f.Commerce.Ean13(),
                    decimal.Parse(f.Commerce.Price(10, 200))
                ));

            return faker.Generate();
        }
        public static CreateProductRequest GenerateFakeCreateProductRequest(string productCode)
        {
            var faker = new Faker<CreateProductRequest>()
                .CustomInstantiator(f => new CreateProductRequest(
                    f.Commerce.ProductName(),
                    productCode,
                    decimal.Parse(f.Commerce.Price(10, 200))
                ));

            return faker.Generate();
        }


        public static UpdateProductRequest GenerateFakeUpdateProductRequest(Guid id)
        {
            var faker = new Faker<UpdateProductRequest>()
                .CustomInstantiator(f => new UpdateProductRequest(
                    id,
                    f.Commerce.ProductName(),
                    decimal.Parse(f.Commerce.Price(10, 200))
                ));

            return faker.Generate();
        }
    }
}
