using CodeHub.Engine.SonarCloud.Models;
using CodeHub.Engine.SonarCloud.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Engine.SonarCloud;

public static class SonarCloudExtensions
{
    public static IServiceCollection RegisterSonarCloudServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<ISonarCloudService, SonarCloudService>();

        services.Configure<SonarCloudSettings>(options =>
        {
            configuration.GetSection("SonarCloudSettings").Bind(options);
        });
        return services;
    }
}