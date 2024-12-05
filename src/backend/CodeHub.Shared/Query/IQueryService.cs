using CodeHub.Shared.Models;
using CodeHub.Shared.Models.Requests;

namespace CodeHub.Shared.Query;

public interface IQueryService
{
    List<Pipeline> QueryPipelines(QueryPipelineRequest request);
    List<Repository> QueryRepositories(QueryRepositoryRequest request);
    List<PullRequest> QueryPullRequests(QueryPullRequestRequest request);
}