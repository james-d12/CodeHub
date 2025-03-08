using CodeHub.Shared;

namespace CodeHub.Module.GitLab.Models;

public sealed class GitLabSettings : Settings
{
    public string HostUrl { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}