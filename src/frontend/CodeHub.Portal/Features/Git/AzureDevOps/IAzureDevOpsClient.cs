using CodeHub.Module.AzureDevOps.Models;

namespace CodeHub.Portal.Features.Git.AzureDevOps;

public interface IAzureDevOpsClient
{
    Task<AzureDevOpsRepository?> GetRepositoryAsync(string name);
    Task<AzureDevOpsPipeline?> GetPipelineAsync(string name);
}