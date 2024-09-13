﻿using CodeHub.Platform.Azure.Extensions;
using CodeHub.Platform.AzureDevOps.Extensions;
using CodeHub.Platform.SonarCloud.Extensions;
using CodeHub.Platform.SooS.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Platform;

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