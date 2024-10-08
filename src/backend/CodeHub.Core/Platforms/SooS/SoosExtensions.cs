using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.Platforms.SooS;

public static class SoosExtensions
{
    public static IServiceCollection RegisterSoosServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.TryAddSingleton<ISoosCacheService, SoosCacheService>();
        services.TryAddScoped<ISoosService, SoosService>();

        services.Configure<SoosSettings>(options => { configuration.GetSection("SoosSettings").Bind(options); });
        return services;
    }
}