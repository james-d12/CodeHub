using CodeHub.Core.Platforms.SonarCloud.Models;
using CodeHub.Core.Platforms.SonarCloud.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Platforms.SonarCloud.Extensions;

public static class SonarCloudExtensions
{
    public static IServiceCollection RegisterSonarCloudServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddTransient<ISonarCloudService, SonarCloudService>();

        services.Configure<SonarCloudSettings>(options =>
        {
            configuration.GetSection(nameof(SonarCloudSettings)).Bind(options);
        });
        return services;
    }
}