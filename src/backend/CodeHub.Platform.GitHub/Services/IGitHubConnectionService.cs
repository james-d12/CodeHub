using Octokit;

namespace CodeHub.Platform.GitHub.Services;

internal interface IGitHubConnectionService
{
    GitHubClient Client { get; }
}