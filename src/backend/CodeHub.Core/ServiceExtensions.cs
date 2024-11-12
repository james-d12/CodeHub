using CodeHub.Core.Platforms.Azure;
using CodeHub.Core.Platforms.Azure.Extensions;
using CodeHub.Core.Platforms.AzureDevOps;
using CodeHub.Core.Platforms.AzureDevOps.Extensions;
using CodeHub.Core.Platforms.SonarCloud;
using CodeHub.Core.Platforms.SonarCloud.Extensions;
using CodeHub.Core.Platforms.Soos.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Core;

public static class ServiceExtensions
{
    public static void RegisterPlatforms(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterSonarCloudServices(configuration);
        services.RegisterAzureDevOpsServices(configuration);
        services.RegisterSoosServices(configuration);
        services.RegisterAzureServices();
    }
}