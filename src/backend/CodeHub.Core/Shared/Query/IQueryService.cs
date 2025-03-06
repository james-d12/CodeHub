using CodeHub.Core.Shared.Models;
using CodeHub.Core.Shared.Query.Requests;
using PipelineQueryRequest = CodeHub.Core.Shared.Query.Requests.PipelineQueryRequest;

namespace CodeHub.Core.Shared.Query;

public interface IQueryService
{
    List<Pipeline> QueryPipelines(PipelineQueryRequest request);
    List<Repository> QueryRepositories(RepositoryQueryRequest request);
    List<PullRequest> QueryPullRequests(PullRequestQueryRequest request);
}