using CodeHub.Core.GitLab.Models;
using CodeHub.Core.GitLab.Services;
using CodeHub.Core.GitLab.Validator;
using CodeHub.Core.Shared.Query;
using CodeHub.Core.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CodeHub.Core.GitLab.Extensions;

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
        services.AddScoped<IQueryService, GitLabQueryService>();
        services.AddSingleton<IDiscoveryService, GitLabDiscoveryService>();
    }

    private static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<GitLabSettings>()
            .Bind(configuration.GetRequiredSection(nameof(GitLabSettings)));
    }
}