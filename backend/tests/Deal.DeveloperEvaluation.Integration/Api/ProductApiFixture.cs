using Deal.DeveloperEvaluation.Integration.TestData;
using Deal.DeveloperEvaluation.WebApi;
using Deal.DeveloperEvaluation.WebApi.Dtos;
using Deal.DeveloperEvaluation.WebApi.UseCases.CreateProduct;
using System.Net.Http.Json;

namespace Deal.DeveloperEvaluation.Integration.Api
{
    public class ProductApiFixture : IDisposable
    {
        public HttpClient Client { get; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;

        public ProductApiFixture()
        {
            var factory = new CustomWebApplicationFactory();
            Client = factory.CreateClient();

            var response = Client.PostAsJsonAsync("/api/product", ProductFaker.GenerateFakeCreateProductRequest()).Result;
            var product = response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResult>>().Result;
            ProductId = (Guid)(product!.Data!.Id);
            ProductName = product!.Data!.Name;
        }

        public void Dispose()
        {
            Client.DeleteAsync($"/api/products/{ProductId}").Wait();
        }
    }
}
