using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Platform.AzureDevOps.Services;
using CodeHub.Platform.AzureDevOps.Validation;
using CodeHub.Shared.Query;
using CodeHub.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace CodeHub.Platform.AzureDevOps.Extensions;

public static class AzureDevOpsExtensions
{
    public static IServiceCollection RegisterAzureDevOps(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (!IsAzureDevOpsEnabled(configuration))
        {
            return services;
        }

        services.RegisterCache();
        services.RegisterServices();
        services.RegisterOptions(configuration);
        return services;
    }

    private static bool IsAzureDevOpsEnabled(IConfiguration configuration)
    {
        const string key = $"{nameof(AzureDevOpsSettings)}:{nameof(AzureDevOpsSettings.IsEnabled)}";
        return configuration.GetValue<bool>(key);
    }

    private static void RegisterCache(this IServiceCollection services)
    {
        services.AddMemoryCache(options => options.TrackStatistics = true);
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IAzureDevOpsService, AzureDevOpsService>();
        services.TryAddSingleton<IQueryService, AzureDevOpsQueryService>();
        services.TryAddSingleton<IAzureDevOpsQueryService, AzureDevOpsQueryService>();
        services.TryAddSingleton<IAzureDevOpsConnectionService, AzureDevOpsConnectionService>();
        services.TryAddSingleton<IDiscoveryService, AzureDevOpsDiscoveryService>();
    }

    private static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<IValidateOptions<AzureDevOpsSettings>, AzureDevOpsSettingsValidation>();

        services.AddOptions<AzureDevOpsSettings>()
            .Bind(configuration.GetRequiredSection(nameof(AzureDevOpsSettings)))
            .ValidateOnStart();
    }
}