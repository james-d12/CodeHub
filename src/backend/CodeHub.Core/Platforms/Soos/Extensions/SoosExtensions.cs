using CodeHub.Core.Platforms.Soos.Models;
using CodeHub.Core.Platforms.Soos.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Platforms.Soos.Extensions;

public static class SoosExtensions
{
    public static IServiceCollection RegisterSoosServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.TryAddSingleton<ISoosCacheService, SoosCacheService>();
        services.TryAddScoped<ISoosService, SoosService>();

        services.Configure<SoosSettings>(options => { configuration.GetSection(nameof(SoosSettings)).Bind(options); });
        return services;
    }
}