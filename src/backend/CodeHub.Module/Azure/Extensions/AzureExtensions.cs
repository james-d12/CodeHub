using CodeHub.Domain.Cloud;
using CodeHub.Domain.Cloud.Service;
using CodeHub.Domain.Discovery;
using CodeHub.Domain.Git;
using CodeHub.Module.Azure.Services;
using CodeHub.Module.Azure.Validation;
using CodeHub.Module.AzureDevOps.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Module.Azure.Extensions;

public static class AzureExtensions
{
    public static IServiceCollection RegisterAzure(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = AzureSettingsValidator.GetValidSettings(configuration);

        if (!settings.IsEnabled)
        {
            return services;
        }

        services.RegisterServices();
        services.RegisterCache();
        return services;
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IAzureService, AzureService>();
        services.AddScoped<ICloudQueryService, AzureCloudQueryService>();
        services.AddSingleton<IDiscoveryService, AzureDiscoveryService>();
    }

    private static void RegisterCache(this IServiceCollection services)
    {
        services.AddMemoryCache(options => options.TrackStatistics = true);
    }
}