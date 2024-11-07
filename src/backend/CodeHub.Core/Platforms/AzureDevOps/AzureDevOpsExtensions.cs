using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Platforms.AzureDevOps;

public static class AzureDevOpsExtensions
{
    public static IServiceCollection RegisterAzureDevOpsServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.TryAddScoped<IAzureDevOpsService, AzureDevOpsService>();

        services.Configure<AzureDevOpsSettings>(options =>
        {
            configuration.GetSection("AzureDevOpsSettings").Bind(options);
        });
        return services;
    }
}