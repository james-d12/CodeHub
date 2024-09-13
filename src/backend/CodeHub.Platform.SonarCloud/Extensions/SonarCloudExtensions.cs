using CodeHub.Platform.SonarCloud.Models;
using CodeHub.Platform.SonarCloud.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Platform.SonarCloud.Extensions;

public static class SonarCloudExtensions
{
    public static IServiceCollection RegisterSonarCloudServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.TryAddScoped<ISonarCloudService, SonarCloudService>();

        services.Configure<SonarCloudSettings>(options =>
        {
            configuration.GetSection("SonarCloudSettings").Bind(options);
        });
        return services;
    }
}