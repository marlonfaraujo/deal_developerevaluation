using Deal.DeveloperEvaluation.WebApi.Dtos;
using System.Text.Json;

namespace Deal.DeveloperEvaluation.WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidOperationException ex)
            {
                var response = new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                };
                await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest, response);
            }
            catch (ArgumentException ex)
            {
                var response = new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                };
                await HandleExceptionAsync(context, ex, StatusCodes.Status400BadRequest, response);
            }
            catch (KeyNotFoundException ex)
            {
                var response = new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                };
                await HandleExceptionAsync(context, ex, StatusCodes.Status404NotFound, response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse
                {
                    Success = false,
                    Message = ex.Message
                };
                await HandleExceptionAsync(context, ex, StatusCodes.Status500InternalServerError, response);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, ApiResponse response)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            Console.WriteLine($"Handler Exception: {exception.Message}");
            return context.Response.WriteAsync(JsonSerializer.Serialize(response, jsonOptions));
        }
    }
}
