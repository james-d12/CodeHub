using CodeHub.Domain.Discovery;
using CodeHub.Domain.Git.Service;
using CodeHub.Module.GitHub.Models;
using CodeHub.Module.GitHub.Services;
using CodeHub.Module.GitHub.Validator;
using CodeHub.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Module.GitHub.Extensions;

public static class GitHubExtensions
{
    public static IServiceCollection RegisterGitHub(this IServiceCollection services,
        IConfiguration configuration)
    {
        using var activity = Tracing.StartActivity();
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
        services.AddScoped<IGitQueryService, GitHubGitQueryService>();
        services.AddSingleton<IDiscoveryService, GitHubDiscoveryService>();
    }

    private static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<GitHubSettings>()
            .Bind(configuration.GetRequiredSection(nameof(GitHubSettings)));
    }
}