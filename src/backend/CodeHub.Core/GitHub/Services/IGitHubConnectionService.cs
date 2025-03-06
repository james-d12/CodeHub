using Octokit;

namespace CodeHub.Core.GitHub.Services;

public interface IGitHubConnectionService
{
    GitHubClient Client { get; }
}