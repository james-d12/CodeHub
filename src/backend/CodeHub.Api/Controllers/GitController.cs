using CodeHub.Domain.Git;
using CodeHub.Domain.Git.Request;
using CodeHub.Domain.Git.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("git/")]
public sealed class GitController : ControllerBase
{
    private readonly IEnumerable<IGitQueryService> _queryServices;

    public GitController(IEnumerable<IGitQueryService> queryServices)
    {
        _queryServices = queryServices;
    }

    [HttpGet, Route("pipelines")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<Pipeline> GetPipelines([FromQuery] PipelineQueryRequest request)
    {
        var pipelines = new List<Pipeline>();
        foreach (var queryService in _queryServices)
        {
            pipelines.AddRange(queryService.QueryPipelines(request));
        }

        return pipelines;
    }

    [HttpGet, Route("repositories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<Repository> GetRepositories([FromQuery] RepositoryQueryRequest request)
    {
        var repositories = new List<Repository>();
        foreach (var queryService in _queryServices)
        {
            repositories.AddRange(queryService.QueryRepositories(request));
        }

        return repositories;
    }

    [HttpGet, Route("pull-requests")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<PullRequest> GetPullRequests([FromQuery] PullRequestQueryRequest request)
    {
        var pullRequests = new List<PullRequest>();
        foreach (var queryService in _queryServices)
        {
            pullRequests.AddRange(queryService.QueryPullRequests(request));
        }

        return pullRequests;
    }
}