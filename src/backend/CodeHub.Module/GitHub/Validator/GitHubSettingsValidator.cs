using CodeHub.Module.GitHub.Models;
using CodeHub.Module.Shared.Validation;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Module.GitHub.Validator;

internal static class GitHubSettingsValidator
{
    internal static GitHubSettings GetValidSettings(IConfiguration configuration)
    {
        return new ValidationBuilder<GitHubSettings>(configuration)
            .SectionExists(nameof(GitHubSettings))
            .CheckEnabled(x => x.IsEnabled, nameof(GitHubSettings.IsEnabled))
            .CheckValue(x => x.AgentName, nameof(GitHubSettings.AgentName))
            .CheckValue(x => x.Token, nameof(GitHubSettings.Token))
            .Build();
    }
}