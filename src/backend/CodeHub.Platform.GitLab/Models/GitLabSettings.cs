using CodeHub.Shared.Models;

namespace CodeHub.Platform.GitLab.Models;

internal sealed class GitLabSettings : Settings
{
    public string HostUrl { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}