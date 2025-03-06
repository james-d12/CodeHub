using CodeHub.Core.AzureDevOps.Models;
using CodeHub.Core.AzureDevOps.Services;
using CodeHub.Core.AzureDevOps.Validation;
using CodeHub.Core.Shared.Query;
using CodeHub.Core.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.AzureDevOps.Extensions;

public static class AzureDevOpsExtensions
{
    public static IServiceCollection RegisterAzureDevOps(this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = AzureDevOpsSettingsValidator.GetValidSettings(configuration);

        if (!settings.IsEnabled)
        {
            return services;
        }

        services.RegisterCache();
        services.RegisterServices();
        services.RegisterOptions(configuration);
        return services;
    }

    private static void RegisterCache(this IServiceCollection services)
    {
        services.AddMemoryCache(options => options.TrackStatistics = true);
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IAzureDevOpsService, AzureDevOpsService>();
        services.TryAddSingleton<IAzureDevOpsConnectionService, AzureDevOpsConnectionService>();
        services.AddScoped<IQueryService, AzureDevOpsQueryService>();
        services.AddSingleton<IDiscoveryService, AzureDevOpsDiscoveryService>();
    }

    private static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<AzureDevOpsSettings>()
            .Bind(configuration.GetRequiredSection(nameof(AzureDevOpsSettings)));
    }
}