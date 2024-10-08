using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Platforms.Azure;

public static class AzureExtensions
{
    public static IServiceCollection RegisterAzureServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.TryAddSingleton<IAzureCacheService, AzureCacheService>();
        services.TryAddSingleton<IAzureService, AzureService>();
        return services;
    }
}