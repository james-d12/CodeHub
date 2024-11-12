using CodeHub.Core.Models;
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
        services.TryAddScoped<IAzureDevOpsService, AzureDevOpsService>();
        services.TryAddKeyedScoped<IDiscoveryService, AzureDevOpsDiscoveryService>(DiscoveryServiceType.AzureDevOps);
        services.Configure<AzureDevOpsSettings>(options =>
        {
            configuration.GetSection(nameof(AzureDevOpsSettings)).Bind(options);
        });

        return services;
    }
}