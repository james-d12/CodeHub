using CodeHub.Core.Platforms.Soos.Models;
using CodeHub.Core.Platforms.Soos.Services;
using CodeHub.Core.Platforms.Soos.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace CodeHub.Core.Platforms.Soos.Extensions;

public static class SoosExtensions
{
    public static IServiceCollection RegisterSoosServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Services
        services.AddMemoryCache();
        services.TryAddSingleton<ISoosCacheService, SoosCacheService>();
        services.TryAddTransient<ISoosService, SoosService>();
        services.TryAddSingleton<IValidateOptions<SoosSettings>, SoosSettingsValidation>();

        // Register Options
        services.AddOptions<SoosSettings>()
            .Bind(configuration.GetRequiredSection(nameof(SoosSettings)))
            .ValidateOnStart();

        return services;
    }
}