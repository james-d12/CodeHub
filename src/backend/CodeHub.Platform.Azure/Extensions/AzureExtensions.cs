using CodeHub.Platform.Azure.Models;
using CodeHub.Platform.Azure.Services;
using CodeHub.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Platform.Azure.Extensions;

public static class AzureExtensions
{
    public static IServiceCollection RegisterAzure(this IServiceCollection services, IConfiguration configuration)
    {
        if (!IsAzureEnabled(configuration))
        {
            return services;
        }

        services.RegisterCache();
        services.TryAddSingleton<IAzureService, AzureService>();
        services.TryAddSingleton<IDiscoveryService, AzureDiscoveryService>();
        return services;
    }

    private static bool IsAzureEnabled(IConfiguration configuration)
    {
        const string key = $"{nameof(AzureSettings)}:{nameof(AzureSettings.IsEnabled)}";
        return configuration.GetValue<bool>(key);
    }

    private static void RegisterCache(this IServiceCollection services)
    {
        services.AddMemoryCache(options => options.TrackStatistics = true);
    }
}