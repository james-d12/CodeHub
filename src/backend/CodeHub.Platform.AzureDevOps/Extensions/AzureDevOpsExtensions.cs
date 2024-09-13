using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Platform.AzureDevOps.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Platform.AzureDevOps.Extensions;

public static class AzureDevOpsExtensions
{
    public static IServiceCollection RegisterAzureDevOpsServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.TryAddSingleton<IAzureDevOpsCacheService, AzureDevOpsCacheService>();
        services.TryAddScoped<IAzureDevOpsService, AzureDevOpsService>();

        services.Configure<AzureDevOpsSettings>(options =>
        {
            configuration.GetSection("AzureDevOpsSettings").Bind(options);
        });
        return services;
    }
}