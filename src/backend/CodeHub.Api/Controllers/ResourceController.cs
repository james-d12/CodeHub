﻿using CodeHub.Shared.Models;
using CodeHub.Shared.Query;
using CodeHub.Shared.Query.Requests;
using Microsoft.AspNetCore.Mvc;
using PipelineQueryRequest = CodeHub.Shared.Query.Requests.PipelineQueryRequest;

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