using CodeHub.Core.Services;
using CodeHub.Platform.Azure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Platform.Azure.Extensions;

public static class AzureExtensions
{
    public static IServiceCollection RegisterAzureServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.TryAddTransient<IAzureService, AzureService>();
        services.TryAddSingleton<IDiscoveryService, AzureDiscoveryService>();
        return services;
    }
}