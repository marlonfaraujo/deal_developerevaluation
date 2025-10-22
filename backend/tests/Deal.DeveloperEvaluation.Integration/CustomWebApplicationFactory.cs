using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Deal.DeveloperEvaluation.WebApi
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
            builder.ConfigureAppConfiguration((context, configBuilder) =>
            {
                configBuilder.Sources.Clear();

                configBuilder
                    .AddEnvironmentVariables();
            });
        }
    }
}
