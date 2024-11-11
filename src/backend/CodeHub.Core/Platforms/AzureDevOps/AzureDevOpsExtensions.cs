using CodeHub.Core.Models;
using CodeHub.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Platforms.AzureDevOps;

public static class AzureDevOpsExtensions
{
    public static IServiceCollection RegisterAzureDevOpsServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.TryAddTransient<IAzureDevOpsService, AzureDevOpsService>();
        services.TryAddKeyedTransient<IDiscoveryService, AzureDevOpsDiscoveryService>(DiscoveryServiceType.AzureDevOps);
        services.Configure<AzureDevOpsSettings>(options =>
        {
            configuration.GetSection("AzureDevOpsSettings").Bind(options);
        });

        return services;
    }
}