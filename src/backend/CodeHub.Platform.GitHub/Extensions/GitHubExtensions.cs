using CodeHub.Platform.GitHub.Models;
using CodeHub.Platform.GitHub.Services;
using CodeHub.Platform.GitHub.Validator;
using CodeHub.Shared.Query;
using CodeHub.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Platform.GitHub.Extensions;

public static class GitHubExtensions
{
    public static IServiceCollection RegisterGitHub(this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = GitHubSettingsValidator.GetValidSettings(configuration);

        if (!settings.IsEnabled)
        {
            return services;
        }

        services.RegisterCache();
        services.RegisterServices();
        services.RegisterOptions(configuration);
        return services;
    }

    private static void RegisterCache(this IServiceCollection services)
    {
        services.AddMemoryCache(options => options.TrackStatistics = true);
    }

    private static void RegisterServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IGitHubConnectionService, GitHubConnectionService>();
        services.TryAddSingleton<IGitHubService, GitHubService>();
        services.AddScoped<IQueryService, GitHubQueryService>();
        services.AddSingleton<IDiscoveryService, GitHubDiscoveryService>();
    }

    private static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<GitHubSettings>()
            .Bind(configuration.GetRequiredSection(nameof(GitHubSettings)));
    }
}