using CodeHub.Domain.Discovery;
using CodeHub.Domain.Git;
using CodeHub.Domain.Git.Service;
using CodeHub.Module.GitLab.Models;
using CodeHub.Module.GitLab.Services;
using CodeHub.Module.GitLab.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Module.GitLab.Extensions;

public static class GitLabExtensions
{
    public static IServiceCollection RegisterGitLab(this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = GitLabSettingsValidator.GetValidSettings(configuration);

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
        services.TryAddSingleton<IGitLabConnectionService, GitLabConnectionService>();
        services.TryAddSingleton<IGitLabService, GitLabService>();
        services.AddScoped<IGitQueryService, GitLabGitQueryService>();
        services.AddSingleton<IDiscoveryService, GitLabDiscoveryService>();
    }

    private static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<GitLabSettings>()
            .Bind(configuration.GetRequiredSection(nameof(GitLabSettings)));
    }
}