using CodeHub.Core.Platforms.AzureDevOps.Models;
using CodeHub.Core.Platforms.AzureDevOps.Services;
using CodeHub.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Platforms.AzureDevOps.Extensions;

public static class AzureDevOpsExtensions
{
    public static IServiceCollection RegisterAzureDevOpsServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.TryAddTransient<IAzureDevOpsService, AzureDevOpsService>();
        services.TryAddSingleton<IDiscoveryService, AzureDevOpsDiscoveryService>();
        services.Configure<AzureDevOpsSettings>(options =>
        {
            configuration.GetSection(nameof(AzureDevOpsSettings)).Bind(options);
        });

        return services;
    }
}