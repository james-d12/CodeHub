using CodeHub.Module.GitHub.Models;

namespace CodeHub.Module.GitHub.Services;

public interface IGitHubService
{
    Task<List<GitHubRepository>> GetRepositoriesAsync(CancellationToken cancellationToken);
    Task<List<GitHubPipeline>> GetPipelinesAsync(GitHubRepository repository, CancellationToken cancellationToken);

    Task<List<GitHubPullRequest>> GetPullRequestsAsync(GitHubRepository repository);
}