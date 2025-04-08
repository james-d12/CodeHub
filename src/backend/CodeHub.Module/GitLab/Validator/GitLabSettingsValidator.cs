using CodeHub.Module.GitLab.Models;
using CodeHub.Module.Shared.Validation;
using CodeHub.Shared;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Module.GitLab.Validator;

internal static class GitLabSettingsValidator
{
    internal static GitLabSettings GetValidSettings(IConfiguration configuration)
    {
        using var activity = Tracing.StartActivity();
        return new ValidationBuilder<GitLabSettings>(configuration)
            .SectionExists(nameof(GitLabSettings))
            .CheckEnabled(x => x.IsEnabled, nameof(GitLabSettings.IsEnabled))
            .CheckValue(x => x.HostUrl, nameof(GitLabSettings.HostUrl))
            .CheckValue(x => x.Token, nameof(GitLabSettings.Token))
            .Build();
    }
}