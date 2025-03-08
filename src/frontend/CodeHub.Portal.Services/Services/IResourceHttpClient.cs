using CodeHub.Domain.Git;

namespace CodeHub.Portal.Services.Services;

public interface IResourceHttpClient
{
    Task<List<Pipeline>> GetPipelines();
    Task<List<Repository>> GetRepositories();
    Task<List<PullRequest>> GetPullRequests();
}