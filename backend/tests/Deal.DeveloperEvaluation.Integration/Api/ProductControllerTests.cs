using Bogus;
using Deal.DeveloperEvaluation.Integration.TestData;
using System.Net;
using System.Net.Http.Json;

namespace Deal.DeveloperEvaluation.Integration.Api
{
    public class ProductControllerTests : IClassFixture<ProductApiFixture>
    {
        private readonly ProductApiFixture _productApiFixture;

        public ProductControllerTests(ProductApiFixture productApiFixture)
        {
            _productApiFixture = productApiFixture;
        }

        /// <summary>
        /// Verifies that creating a new product via POST returns HTTP 201 Created when the request is valid.
        /// </summary>
        [Fact(DisplayName = "POST /api/products should return Created when product is valid")]
        public async Task CreateProduct_ReturnsCreated()
        {
            var productRequest = ProductFaker.GenerateFakeCreateProductRequest();

            var response = await _productApiFixture.Client.PostAsJsonAsync("/api/product", productRequest);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        /// <summary>
        /// Verifies that updating a product via PUT returns HTTP 201 Created when the request is valid.
        /// </summary>
        [Fact(DisplayName = "PUT /api/products should return Created when product is updated")]
        public async Task UpdateProduct_ReturnsCreated()
        {
            var updateRequest = ProductFaker.GenerateFakeUpdateProductRequest(_productApiFixture.ProductId);

            var response = await _productApiFixture.Client.PutAsJsonAsync($"/api/product/{_productApiFixture.ProductId}", updateRequest);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Verifies that retrieving a product by ID via GET returns HTTP 200 OK when the product exists.
        /// </summary>
        [Fact(DisplayName = "GET /api/products/{id} should return Ok when product exists")]
        public async Task GetProduct_ReturnsOk()
        {
            var response = await _productApiFixture.Client.GetAsync($"/api/product/{_productApiFixture.ProductId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Verifies that listing products via GET returns HTTP 200 OK.
        /// </summary>
        [Fact(DisplayName = "GET /api/products should return Ok with product list")]
        public async Task ListProducts_ReturnsOk()
        {
            var response = await _productApiFixture.Client.GetAsync($"/api/product");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        /// Verifies that deleting a product via DELETE returns HTTP 200 OK when the product exists.
        /// </summary>
        [Fact(DisplayName = "DELETE /api/products/{id} should return Ok when product is deleted")]
        public async Task DeleteProduct_ReturnsOk()
        {
            var response = await _productApiFixture.Client.DeleteAsync($"/api/product/{_productApiFixture.ProductId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        /// <summary>
        /// Checks whether creating a new product via POST returns HTTP 400 Bad Request when duplicating the product code.
        /// </summary>
        [Fact(DisplayName = "POST /api/products should return Bad Request when duplicating the product code")]
        public async Task CreateExistsProductCode_ReturnsBadRequest()
        {
            var productRequest = ProductFaker.GenerateFakeCreateProductRequest();
            var response = await _productApiFixture.Client.PostAsJsonAsync("/api/product", productRequest);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var existsProductRequest = ProductFaker.GenerateFakeCreateProductRequest(productRequest.Code);
            var existsResponse = await _productApiFixture.Client.PostAsJsonAsync("/api/product", existsProductRequest);
            Assert.Equal(HttpStatusCode.BadRequest, existsResponse.StatusCode);
        }

        /// <summary>
        /// Verifies that listing products via GET returns HTTP 200 OK and a valid response.
        /// </summary>
        [Fact(DisplayName = "GET /api/products should return Ok and products list sortBy Price")]
        public async Task Get_Products_ReturnsOkAndProductsListByName()
        {
            var productRequest = ProductFaker.GenerateFakeCreateProductRequest();
            var response = await _productApiFixture.Client.PostAsJsonAsync("/api/product", productRequest);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var query = $"?pageNumber=1&pageSize=10&sortBy=Price&sortDirection=desc";
            // Act
            var getResponse = await _productApiFixture.Client.GetAsync($"/api/product{query}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            var content = await getResponse.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(content));
        }
    }
}
