using CodeHub.Core.GitHub.Models;

namespace CodeHub.Core.GitHub.Services;

public interface IGitHubService
{
    Task<List<GitHubRepository>> GetRepositoriesAsync(CancellationToken cancellationToken);
    Task<List<GitHubPipeline>> GetActionsAsync(string owner, string repository, CancellationToken cancellationToken);

    Task<List<GitHubPullRequest>> GetPullRequestsAsync(GitHubRepository repository);
}