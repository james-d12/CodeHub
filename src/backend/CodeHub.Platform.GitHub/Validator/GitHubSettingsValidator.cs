using CodeHub.Platform.GitHub.Models;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Platform.GitHub.Validator;

internal static class GitHubSettingsValidator
{
    internal static GitHubSettings GetValidSettings(IConfiguration configuration)
    {
        var settingsSection = configuration.GetSection(nameof(GitHubSettings));
        var isEnabledSection = settingsSection.GetSection(nameof(GitHubSettings.IsEnabled));

        if (!settingsSection.Exists() || !isEnabledSection.Exists())
        {
            throw new InvalidOperationException("");
        }

        var agentName = settingsSection.GetValue<string>(nameof(GitHubSettings.AgentName));
        var token = settingsSection.GetValue<string>(nameof(GitHubSettings.Token));
        var isEnabled = settingsSection.GetValue<bool>(nameof(GitHubSettings.IsEnabled));

        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(agentName))
        {
            throw new InvalidOperationException("");
        }

        return new GitHubSettings
        {
            AgentName = agentName,
            Token = token,
            IsEnabled = isEnabled,
        };
    }
}