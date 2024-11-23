using CodeHub.Core.Platforms.AzureDevOps.Models;
using CodeHub.Core.Platforms.AzureDevOps.Services;
using CodeHub.Core.Platforms.AzureDevOps.Validation;
using CodeHub.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace CodeHub.Core.Platforms.AzureDevOps.Extensions;

public static class AzureDevOpsExtensions
{
    public static IServiceCollection RegisterAzureDevOpsServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Services
        services.AddMemoryCache();
        services.TryAddSingleton<IAzureDevOpsConnectionService, AzureDevOpsConnectionService>();
        services.TryAddTransient<IAzureDevOpsService, AzureDevOpsService>();
        services.TryAddSingleton<IDiscoveryService, AzureDevOpsDiscoveryService>();
        services.TryAddSingleton<IValidateOptions<AzureDevOpsSettings>, AzureDevOpsSettingsValidation>();

        // Register Options
        services.AddOptions<AzureDevOpsSettings>()
            .Bind(configuration.GetRequiredSection(nameof(AzureDevOpsSettings)))
            .ValidateOnStart();

        return services;
    }
}