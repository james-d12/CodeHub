using CodeHub.Core.GitHub.Models;
using CodeHub.Core.GitHub.Services;
using CodeHub.Core.GitHub.Validator;
using CodeHub.Core.Shared.Query;
using CodeHub.Core.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.GitHub.Extensions;

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