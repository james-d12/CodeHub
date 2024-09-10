using CodeHub.Engine.AzureDevOps.Models;

namespace CodeHub.Engine.AzureDevOps.Services;

internal interface IAzureDevOpsCacheService
{
    void SetRepositories(List<AzureDevOpsRepository> repositories);
    void SetPipelines(List<AzureDevOpsPipeline> pipelines);
    List<AzureDevOpsRepository> GetRepositories();
    List<AzureDevOpsPipeline> GetPipelines();
}