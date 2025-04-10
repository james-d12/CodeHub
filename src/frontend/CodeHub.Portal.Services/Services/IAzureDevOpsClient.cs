using CodeHub.Module.AzureDevOps.Models;

namespace CodeHub.Portal.Services.Services;

public interface IAzureDevOpsClient
{
    Task<AzureDevOpsRepository?> GetRepositoryAsync(string name);
    Task<AzureDevOpsPipeline?> GetPipelineAsync(string name);
}