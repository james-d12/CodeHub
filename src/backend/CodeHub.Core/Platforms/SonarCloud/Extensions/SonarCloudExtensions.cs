using CodeHub.Core.Platforms.SonarCloud.Models;
using CodeHub.Core.Platforms.SonarCloud.Services;
using CodeHub.Core.Platforms.SonarCloud.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace CodeHub.Core.Platforms.SonarCloud.Extensions;

public static class SonarCloudExtensions
{
    public static IServiceCollection RegisterSonarCloudServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Services
        services.TryAddTransient<ISonarCloudService, SonarCloudService>();
        services.TryAddSingleton<IValidateOptions<SonarCloudSettings>, SonarCloudSettingsValidation>();

        // Register Options
        services.AddOptions<SonarCloudSettings>()
            .Bind(configuration.GetRequiredSection(nameof(SonarCloudSettings)))
            .ValidateOnStart();

        return services;
    }
}