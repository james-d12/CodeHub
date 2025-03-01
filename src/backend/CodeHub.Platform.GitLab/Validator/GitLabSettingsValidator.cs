using CodeHub.Platform.GitLab.Models;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Platform.GitLab.Validator;

internal static class GitLabSettingsValidator
{
    internal static GitLabSettings GetValidSettings(IConfiguration configuration)
    {
        var settingsSection = configuration.GetSection(nameof(GitLabSettings));

        if (!settingsSection.Exists())
        {
            throw new InvalidOperationException("GitLab settings section is missing");
        }

        var isEnabledSection = settingsSection.GetSection(nameof(GitLabSettings.IsEnabled));
        var isEnabled = settingsSection.GetValue<bool>(nameof(GitLabSettings.IsEnabled));

        if (!isEnabledSection.Exists() || !isEnabled)
        {
            return GitLabSettings.CreateDisabled();
        }

        var hostUrl = settingsSection.GetValue<string>(nameof(GitLabSettings.HostUrl));
        var token = settingsSection.GetValue<string>(nameof(GitLabSettings.Token));

        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(hostUrl))
        {
            throw new InvalidOperationException("");
        }

        return new GitLabSettings
        {
            HostUrl = hostUrl,
            Token = token,
            IsEnabled = isEnabled,
        };
    }
}