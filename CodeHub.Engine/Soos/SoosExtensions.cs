using CodeHub.Engine.SooS.Models;
using CodeHub.Engine.SooS.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHub.Engine.Soos;

public static class SoosExtensions
{
    public static IServiceCollection RegisterSoosServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddSingleton<ISoosCacheService, SoosCacheService>();
        services.AddScoped<ISoosService, SoosService>();

        services.Configure<SoosSettings>(options =>
        {
            configuration.GetSection("SoosSettings").Bind(options);
        });
        return services;
    }
}