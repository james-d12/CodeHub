﻿using CodeHub.Platform.GitHub.Extensions;
using CodeHub.Shared.Models;
using Repository = CodeHub.Shared.Models.Repository;

namespace CodeHub.Platform.GitHub.Services;

internal sealed class GitHubService : IGitHubService
{
    private readonly IGitHubConnectionService _gitHubConnectionService;

    public GitHubService(IGitHubConnectionService gitHubConnectionService)
    {
        _gitHubConnectionService = gitHubConnectionService;
    }

    public async Task<List<Repository>> GetRepositoriesAsync(CancellationToken cancellationToken)
    {
        var repositories =
            await _gitHubConnectionService.Client().Repository.GetAllForCurrent().WaitAsync(cancellationToken) ?? [];
        return repositories.Select(r => r.MapToRepository()).ToList();
    }

    public async Task<List<Pipeline>> GetActionsAsync(string owner, string repository,
        CancellationToken cancellationToken)
    {
        var pipelines = await _gitHubConnectionService.Client().Actions.Workflows.List(owner, repository);
        var jobs = await _gitHubConnectionService.Client().Actions.Workflows.Runs.List(owner, repository);

        return pipelines.Workflows.Select(w => w.MapToPipeline()).ToList();
    }
}