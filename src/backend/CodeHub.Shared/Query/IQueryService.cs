using CodeHub.Shared.Models;
using CodeHub.Shared.Query.Requests;
using PipelineQueryRequest = CodeHub.Shared.Query.Requests.PipelineQueryRequest;

namespace CodeHub.Shared.Query;

public interface IQueryService
{
    List<Pipeline> QueryPipelines(PipelineQueryRequest request);
    List<Repository> QueryRepositories(RepositoryQueryRequest request);
    List<PullRequest> QueryPullRequests(PullRequestQueryRequest request);
}