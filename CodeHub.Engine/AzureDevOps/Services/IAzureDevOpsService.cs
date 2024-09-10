﻿using CodeHub.Engine.AzureDevOps.Models;

namespace CodeHub.Engine.AzureDevOps.Services;

public interface IAzureDevOpsService
{
    Task<List<AzureDevOpsRepository>> GetRepositoriesAsync(string projectName, CancellationToken cancellationToken);
    Task<List<AzureDevOpsPipeline>> GetPipelinesAsync(string projectName, CancellationToken cancellationToken);
    Task<List<AzureDevOpsProject>> GetProjectsAsync(CancellationToken cancellationToken);
    Task<List<AzureDevOpsTeam>> GetTeamsAsync(CancellationToken cancellationToken);
}