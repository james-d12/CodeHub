using CodeHub.Platform.GitHub.Models;
using CodeHub.Shared.Validation;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Platform.GitHub.Validator;

internal static class GitHubSettingsValidator
{
    internal static GitHubSettings GetValidSettings(IConfiguration configuration)
    {
        //return new ValidationBuilder<GitHubSettings>(configuration)
        //    .SectionExists(nameof(GitHubSettings))
        //    .CheckEnabled(x => x.IsEnabled, nameof(GitHubSettings.IsEnabled))
        //    .CheckValue(x => x.AgentName, nameof(GitHubSettings.AgentName))
        //    .CheckValue(x => x.Token, nameof(GitHubSettings.Token))
        //    .Build();
        
        var settingsSection = configuration.GetSection(nameof(GitHubSettings));

        if (!settingsSection.Exists())
        {
            throw new InvalidOperationException("GitHub settings section is missing");
        }

        var isEnabledSection = settingsSection.GetSection(nameof(GitHubSettings.IsEnabled));
        var isEnabled = settingsSection.GetValue<bool>(nameof(GitHubSettings.IsEnabled));

        if (!isEnabledSection.Exists() || !isEnabled)
        {
            return GitHubSettings.CreateDisabled();
        }

        var agentName = settingsSection.GetValue<string>(nameof(GitHubSettings.AgentName));
        var token = settingsSection.GetValue<string>(nameof(GitHubSettings.Token));

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