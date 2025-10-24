using Deal.DeveloperEvaluation.Integration.TestData;
using Deal.DeveloperEvaluation.WebApi;
using Deal.DeveloperEvaluation.WebApi.Database;
using Deal.DeveloperEvaluation.WebApi.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace Deal.DeveloperEvaluation.Integration.Database
{
    public class ProductRepositoryTests
    {
        private readonly CustomWebApplicationFactory _factory;

        public ProductRepositoryTests()
        {
            _factory = new CustomWebApplicationFactory();
        }

        [Fact(DisplayName = "Should create a product successfully")]
        public async Task CreateAsync_ShouldAddProduct()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            var repository = new ProductRepository(db);
            var product = ProductFaker.GenerateFakeProducts(1).First();

            var result = await repository.AddAsync(product);

            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
        }

        [Fact(DisplayName = "Should return product by id when it exists")]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            var product = ProductFaker.GenerateFakeProducts(1).First();
            var repository = new ProductRepository(db);

            await repository.AddAsync(product);

            var result = await repository.GetByIdAsync(product.Id);

            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact(DisplayName = "Should return null when product does not exist")]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            var repository = new ProductRepository(db);

            var result = await repository.GetByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact(DisplayName = "Should delete product when it exists")]
        public async Task DeleteAsync_ShouldRemoveProduct_WhenExists()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            var product = ProductFaker.GenerateFakeProducts(1).First();
            var repository = new ProductRepository(db);

            await repository.AddAsync(product);

            var deleted = await repository.DeleteAsync(product.Id);

            Assert.True(deleted);
            Assert.Null(await repository.GetByIdAsync(product.Id));
        }

        [Fact(DisplayName = "Should return false when deleting non-existent product")]
        public async Task DeleteAsync_ShouldReturnFalse_WhenNotExists()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            var repository = new ProductRepository(db);

            var deleted = await repository.DeleteAsync(Guid.NewGuid());

            Assert.False(deleted);
        }

        [Fact(DisplayName = "Should return products by ids")]
        public async Task ListByIdsAsync_ShouldReturnProducts_WhenIdsExist()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            
            var product1 = ProductFaker.GenerateFakeProducts(1).First();
            var product2 = ProductFaker.GenerateFakeProducts(1).First();
            var repository = new ProductRepository(db);
            await repository.AddAsync(product1);
            await repository.AddAsync(product2);

            var result = await repository.GetAsync(new QueryOptions());

            Assert.NotNull(result);
            Assert.NotNull(result.Items);
            Assert.Contains(result.Items, p => p.Id == product1.Id);
            Assert.Contains(result.Items, p => p.Id == product2.Id);
        }

        [Fact(DisplayName = "Should return equal when updating product name")]
        public async Task UpdateAsync_ShouldReturnEqual_WhenProductName()
        {
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            var product = ProductFaker.GenerateFakeProducts(1).First();
            var repository = new ProductRepository(db);

            await repository.AddAsync(product);
            product.ChangePrice(100.0m);
            var updated = await repository.UpdateAsync(product);

            var current = await repository.GetByIdAsync(product.Id);
            Assert.Equal(current!.Price.Value, updated!.Price.Value);
        }
    }
}
