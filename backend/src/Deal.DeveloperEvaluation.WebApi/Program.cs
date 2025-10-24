using Deal.DeveloperEvaluation.WebApi.Database;
using Deal.DeveloperEvaluation.WebApi.Dtos;
using Deal.DeveloperEvaluation.WebApi.Middleware;
using Deal.DeveloperEvaluation.WebApi.Repositories;
using Deal.DeveloperEvaluation.WebApi.UseCases.CreateProduct;
using Deal.DeveloperEvaluation.WebApi.UseCases.DeleteProduct;
using Deal.DeveloperEvaluation.WebApi.UseCases.GetProductById;
using Deal.DeveloperEvaluation.WebApi.UseCases.ListProduct;
using Deal.DeveloperEvaluation.WebApi.UseCases.UpdateProduct;
using Microsoft.EntityFrameworkCore;

public partial class Program
{
    public static WebApplication BuildApp(string[]? args = null)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<DefaultContext>(options =>
        {
            options.UseInMemoryDatabase("ProductDb");
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<CreateProduct>();
        builder.Services.AddScoped<DeleteProduct>();
        builder.Services.AddScoped<GetProductById>();
        builder.Services.AddScoped<ListProduct>();
        builder.Services.AddScoped<UpdateProduct>();
        
        builder.Services.AddCors();
        var app = builder.Build();
        app.UseMiddleware<ExceptionMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }

        app.MapPost("/api/product", async (CreateProductRequest request, CreateProduct createProduct, CancellationToken cancellationToken) =>
        {
            var response = await createProduct.ExecuteAsync(request, cancellationToken);
            return Results.Created(string.Empty, new ApiResponseWithData<CreateProductResult>
            {
                Success = true,
                Message = "Product created successfully",
                Data = response
            });
        })
        .WithTags("Product")
        .WithSummary("Cadastro de produtos")
        .WithDescription("Permite o cadastramento de produtos.")
        .Produces<ApiResponseWithData<CreateProductResult>>(StatusCodes.Status201Created)
        .Produces<ApiResponse>(StatusCodes.Status400BadRequest);

        app.MapPut("/api/product/{id}", async (Guid id, UpdateProductRequest request, UpdateProduct updateProduct, CancellationToken cancellationToken) =>
        {
            var response = await updateProduct.ExecuteAsync(request, cancellationToken);
            return Results.Ok(new ApiResponseWithData<UpdateProductResult>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = response
            });
        })
        .WithTags("Product")
        .WithSummary("Atualização de produtos")
        .WithDescription("Permite a atualização de produtos.")
        .Produces<ApiResponseWithData<UpdateProductResult>>(StatusCodes.Status200OK)
        .Produces<ApiResponse>(StatusCodes.Status400BadRequest);

        app.MapGet("/api/product", async ([AsParameters] ListProductRequest request, ListProduct listProduct, CancellationToken cancellationToken) =>
        {
            var response = await listProduct.ExecuteAsync(request, cancellationToken);
            return Results.Ok(new ApiResponseWithData<ListProductResult>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = response
            });
        })
        .WithTags("Product")
        .WithSummary("Busca produtos filtrando por parâmetros de query")
        .WithDescription("Permite buscar produtos usando filtros opcionais via query string.")
        .Produces<ApiResponseWithData<ListProductResult>>(StatusCodes.Status200OK);

        app.MapGet("/api/product/{id}", async (Guid id, GetProductById getProductById, CancellationToken cancellationToken) =>
        {
            var response = await getProductById.ExecuteAsync(new GetProductByIdRequest(id), cancellationToken);
            return Results.Ok(new ApiResponseWithData<GetProductByIdResult>
            {
                Success = true,
                Message = "Product retrieved successfully",
                Data = response
            });
        })
        .WithTags("Product")
        .WithSummary("Busca produtos filtrando pelo id")
        .WithDescription("Permite buscar produtos pelo id.")
        .Produces<ApiResponseWithData<GetProductByIdResult>>(StatusCodes.Status200OK);

        app.MapDelete("/api/product/{id}", async (Guid id, DeleteProduct deleteProduct, CancellationToken cancellationToken) =>
        {
            await deleteProduct.ExecuteAsync(new DeleteProductRequest(id), cancellationToken);
            return Results.Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            });
        })
        .WithTags("Product")
        .WithSummary("Remove produtos pelo id")
        .WithDescription("Permite remover produtos pelo id.")
        .Produces<ApiResponse>(StatusCodes.Status200OK)
        .Produces<ApiResponse>(StatusCodes.Status400BadRequest);

        return app;
    }

    public static void Main(string[] args)
    {
        BuildApp(args).Run();
    }
}