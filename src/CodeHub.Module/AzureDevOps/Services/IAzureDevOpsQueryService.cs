using CodeHub.Module.AzureDevOps.Models;

namespace CodeHub.Module.AzureDevOps.Services;

public interface IAzureDevOpsQueryService
{
    AzureDevOpsRepository? GetRepository(string repositoryName);
    AzureDevOpsPipeline? GetPipeline(string pipelineName);
}