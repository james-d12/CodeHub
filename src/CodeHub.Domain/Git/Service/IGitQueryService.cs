using CodeHub.Domain.Git.Request;

namespace CodeHub.Domain.Git.Service;

public interface IGitQueryService
{
    List<Pipeline> QueryPipelines(PipelineQueryRequest request);
    List<Repository> QueryRepositories(RepositoryQueryRequest request);
    List<PullRequest> QueryPullRequests(PullRequestQueryRequest request);
}