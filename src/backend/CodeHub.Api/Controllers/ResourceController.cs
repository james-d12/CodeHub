using CodeHub.Shared.Models;
using CodeHub.Shared.Models.Requests;
using CodeHub.Shared.Query;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("resources/")]
public sealed class ResourceDevOpsController : ControllerBase
{
    private readonly IEnumerable<IQueryService> _queryServices;

    public ResourceDevOpsController(IEnumerable<IQueryService> queryServices)
    {
        _queryServices = queryServices;
    }

    [HttpGet, Route("pipelines")]
    public List<Pipeline> GetPipelines([FromQuery] QueryPipelineRequest request)
    {
        var pipelines = new List<Pipeline>();
        foreach (var queryService in _queryServices)
        {
            pipelines.AddRange(queryService.QueryPipelines(request));
        }

        return pipelines;
    }

    [HttpGet, Route("repositories")]
    public List<Repository> GetRepositories([FromQuery] QueryRepositoryRequest request)
    {
        var repositories = new List<Repository>();
        foreach (var queryService in _queryServices)
        {
            repositories.AddRange(queryService.QueryRepositories(request));
        }

        return repositories;
    }

    [HttpGet, Route("pull-requests")]
    public List<PullRequest> GetPullRequests([FromQuery] QueryPullRequestRequest request)
    {
        var pullRequests = new List<PullRequest>();
        foreach (var queryService in _queryServices)
        {
            pullRequests.AddRange(queryService.QueryPullRequests(request));
        }

        return pullRequests;
    }
}