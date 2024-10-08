namespace CodeHub.Core.Platforms.AzureDevOps;

internal interface IAzureDevOpsCacheService
{
    void SetRepositories(List<AzureDevOpsRepository> repositories);
    void SetPipelines(List<AzureDevOpsPipeline> pipelines);
    List<AzureDevOpsRepository> GetRepositories();
    List<AzureDevOpsPipeline> GetPipelines();
}