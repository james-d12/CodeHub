using CodeHub.Core.GitLab.Models;
using CodeHub.Core.Shared.Validation;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Core.GitLab.Validator;

internal static class GitLabSettingsValidator
{
    internal static GitLabSettings GetValidSettings(IConfiguration configuration)
    {
        return new ValidationBuilder<GitLabSettings>(configuration)
            .SectionExists(nameof(GitLabSettings))
            .CheckEnabled(x => x.IsEnabled, nameof(GitLabSettings.IsEnabled))
            .CheckValue(x => x.HostUrl, nameof(GitLabSettings.HostUrl))
            .CheckValue(x => x.Token, nameof(GitLabSettings.Token))
            .Build();
    }
}