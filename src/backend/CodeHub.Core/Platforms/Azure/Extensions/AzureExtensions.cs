using CodeHub.Core.Models;
using CodeHub.Core.Platforms.Azure.Services;
using CodeHub.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Platforms.Azure.Extensions;

public static class AzureExtensions
{
    public static IServiceCollection RegisterAzureServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.TryAddScoped<IAzureService, AzureService>();
        services.TryAddKeyedScoped<IDiscoveryService, AzureDiscoveryService>(DiscoveryServiceType.Azure);
        return services;
    }
}