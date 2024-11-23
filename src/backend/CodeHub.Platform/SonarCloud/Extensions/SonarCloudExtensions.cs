using CodeHub.Platform.SonarCloud.Models;
using CodeHub.Platform.SonarCloud.Services;
using CodeHub.Platform.SonarCloud.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace CodeHub.Platform.SonarCloud.Extensions;

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