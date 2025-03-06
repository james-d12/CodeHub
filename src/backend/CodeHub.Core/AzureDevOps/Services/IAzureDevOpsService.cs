using CodeHub.Core.AzureDevOps.Models;

namespace CodeHub.Core.AzureDevOps.Services;

public interface IAzureDevOpsService
{
    Task<List<AzureDevOpsRepository>> GetRepositoriesAsync(Guid projectId, CancellationToken cancellationToken);
    Task<List<AzureDevOpsPipeline>> GetPipelinesAsync(Guid projectId, CancellationToken cancellationToken);
    Task<List<AzureDevOpsProject>> GetProjectsAsync(CancellationToken cancellationToken);
    Task<List<AzureDevOpsTeam>> GetTeamsAsync(CancellationToken cancellationToken);
    Task<List<AzureDevOpsWorkItem>> GetWorkItemsAsync(string projectName, CancellationToken cancellationToken);
    Task<List<AzureDevOpsPullRequest>> GetPullRequestsAsync(Guid projectId, CancellationToken cancellationToken);
}