using CodeHub.Module.GitHub.Models;

namespace CodeHub.Module.GitHub.Services;

public interface IGitHubService
{
    Task<List<GitHubRepository>> GetRepositoriesAsync();
    Task<List<GitHubPipeline>> GetPipelinesAsync(GitHubRepository repository);

    Task<List<GitHubPullRequest>> GetPullRequestsAsync(GitHubRepository repository);
}