namespace CodeHub.Platform.GitLab.Models;

internal sealed record GitLabSettings
{
    public required string HostUrl { get; init; }
    public required string Token { get; init; }
    public required bool IsEnabled { get; init; }

    public static GitLabSettings CreateDisabled()
    {
        return new GitLabSettings
        {
            IsEnabled = false,
            HostUrl = string.Empty,
            Token = string.Empty
        };
    }
}