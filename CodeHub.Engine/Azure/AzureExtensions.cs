using CodeHub.Engine.Azure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Engine.Azure;

public static class AzureExtensions
{
    public static IServiceCollection RegisterAzureServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IAzureCacheService, AzureCacheService>();
        services.AddScoped<IAzureService, AzureService>();
        return services;
    }
}