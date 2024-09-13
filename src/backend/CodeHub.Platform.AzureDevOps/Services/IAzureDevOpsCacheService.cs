using CodeHub.Platform.AzureDevOps.Models;

namespace CodeHub.Platform.AzureDevOps.Services;

internal interface IAzureDevOpsCacheService
{
    void SetRepositories(List<AzureDevOpsRepository> repositories);
    void SetPipelines(List<AzureDevOpsPipeline> pipelines);
    List<AzureDevOpsRepository> GetRepositories();
    List<AzureDevOpsPipeline> GetPipelines();
}