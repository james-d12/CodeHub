using CodeHub.Domain.Git;

namespace CodeHub.Portal.Features.Git.Client;

public interface IGitHttpClient
{
    Task<List<Pipeline>> GetPipelinesAsync();
    Task<List<Repository>> GetRepositoriesAsync();
    Task<List<PullRequest>> GetPullRequestsAsync();
}