﻿using CodeHub.Engine.AzureDevOps.Models;
using Microsoft.Extensions.Options;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace CodeHub.Engine.AzureDevOps.Services;

internal sealed class AzureDevOpsService : IAzureDevOpsService
{
    private readonly VssConnection _connection;
    private readonly IAzureDevOpsCacheService _azureDevOpsCacheService;

    public AzureDevOpsService(IAzureDevOpsCacheService azureDevOpsCacheService, IOptions<AzureDevOpsSettings> options)
    {
        _azureDevOpsCacheService = azureDevOpsCacheService;
        var connectionUri = new Uri($"https://dev.azure.com/{options.Value.Organization}");
        var credentials = new VssBasicCredential(string.Empty, options.Value.PersonalAccessToken);
        _connection = new VssConnection(connectionUri, credentials);
    }

    public async Task<List<GitRepository>> GetRepositoriesAsync(string projectName, CancellationToken cancellationToken)
    {
        var cachedRepositories = _azureDevOpsCacheService.GetRepositories();

        if (cachedRepositories.Count >= 1)
        {
            return cachedRepositories;
        }

        var gitClient = await _connection.GetClientAsync<GitHttpClient>(cancellationToken);
        var repositories = await gitClient.GetRepositoriesAsync(projectName, cancellationToken: cancellationToken) ?? [];
        
        _azureDevOpsCacheService.SetRepositories(repositories);

        return repositories;
    }

    public async Task<List<BuildDefinitionReference>> GetPipelinesAsync(string projectName,
        CancellationToken cancellationToken)
    {
        var cachedPipelines = _azureDevOpsCacheService.GetPipelines();

        if (cachedPipelines.Count >= 1)
        {
            return cachedPipelines;
        }
        
        var buildClient = await _connection.GetClientAsync<BuildHttpClient>(cancellationToken);
        var pipelines = await buildClient.GetDefinitionsAsync(projectName, cancellationToken: cancellationToken) ?? [];
        
        _azureDevOpsCacheService.SetPipelines(pipelines);

        return pipelines;
    }

    public async Task<List<TeamProject>> GetProjectsAsync(CancellationToken cancellationToken)
    {
        var projectClient = await _connection.GetClientAsync<ProjectHttpClient>(cancellationToken);
        var results = await projectClient.GetProjects();
        return results.Select(project => new TeamProject(project)).ToList();
    }

    public async Task<List<WebApiTeam>> GetTeamsAsync(CancellationToken cancellationToken)
    {
        var teamClient = await _connection.GetClientAsync<TeamHttpClient>(cancellationToken);
        return await teamClient.GetAllTeamsAsync(cancellationToken: cancellationToken);
    }
}