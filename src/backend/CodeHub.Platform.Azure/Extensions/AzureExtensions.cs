using CodeHub.Shared.Services;
using CodeHub.Platform.Azure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Platform.Azure.Extensions;

public static class AzureExtensions
{
    public static IServiceCollection RegisterAzure(this IServiceCollection services)
    {
        services.AddMemoryCache(options => options.TrackStatistics = true);
        services.TryAddSingleton<IAzureService, AzureService>();
        services.TryAddSingleton<IDiscoveryService, AzureDiscoveryService>();
        return services;
    }
}