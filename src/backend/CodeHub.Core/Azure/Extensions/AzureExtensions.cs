using CodeHub.Core.Azure.Services;
using CodeHub.Core.Azure.Validation;
using CodeHub.Core.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Azure.Extensions;

public static class AzureExtensions
{
    public static IServiceCollection RegisterAzure(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = AzureSettingsValidator.GetValidSettings(configuration);

        if (!settings.IsEnabled)
        {
            return services;
        }

        services.RegisterCache();
        services.TryAddSingleton<IAzureService, AzureService>();
        services.AddSingleton<IDiscoveryService, AzureDiscoveryService>();
        return services;
    }

    private static void RegisterCache(this IServiceCollection services)
    {
        services.AddMemoryCache(options => options.TrackStatistics = true);
    }
}