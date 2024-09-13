using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CodeHub.Platform.AzureDevOps.Services;

internal sealed class AzureDevOpsCacheService(IMemoryCache memoryCache) : IAzureDevOpsCacheService
{
    private const string RepositoryKey = "azure-devops-repositories";
    private const string PipelineKey = "azure-devops-pipelines";

    public void SetRepositories(List<AzureDevOpsRepository> repositories)
    {
        SetItem(repositories, RepositoryKey);
    }

    public void SetPipelines(List<AzureDevOpsPipeline> pipelines)
    {
        SetItem(pipelines, PipelineKey);
    }

    public List<AzureDevOpsRepository> GetRepositories()
    {
        return memoryCache.Get<List<AzureDevOpsRepository>>(RepositoryKey) ?? [];
    }

    public List<AzureDevOpsPipeline> GetPipelines()
    {
        return memoryCache.Get<List<AzureDevOpsPipeline>>(PipelineKey) ?? [];
    }

    private void SetItem<T>(T item, string id)
    {
        if (!memoryCache.TryGetValue(id, out _))
        {
            memoryCache.Set(id, item);
        }
    }
}