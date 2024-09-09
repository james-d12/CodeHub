using CodeHub.Engine.AzureDevOps.Models;
using CodeHub.Engine.AzureDevOps.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Engine.AzureDevOps;

public static class AzureDevOpsExtensions
{
    public static IServiceCollection RegisterAzureDevOpsServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddSingleton<IAzureDevOpsCacheService, AzureDevOpsCacheService>();
        services.AddScoped<IAzureDevOpsService, AzureDevOpsService>();

        services.Configure<AzureDevOpsSettings>(options =>
        {
            configuration.GetSection("AzureDevOpsSettings").Bind(options);
        });
        return services;
    }
}