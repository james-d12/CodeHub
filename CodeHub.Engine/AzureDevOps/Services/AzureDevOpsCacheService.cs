using Microsoft.Extensions.Caching.Memory;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace CodeHub.Engine.AzureDevOps.Services;

internal sealed class AzureDevOpsCacheService(IMemoryCache memoryCache) : IAzureDevOpsCacheService
{
    private const string RepositoryKey = "azure-devops-repositories";
    private const string PipelineKey = "azure-devops-pipelines";

    public void SetRepositories(List<GitRepository> repositories)
    {
        SetItem(repositories, RepositoryKey);
    }

    public void SetPipelines(List<BuildDefinitionReference> pipelines)
    {
        SetItem(pipelines, PipelineKey);
    }

    public List<GitRepository> GetRepositories()
    {
        return memoryCache.Get<List<GitRepository>>(RepositoryKey) ?? [];
    }

    public List<BuildDefinitionReference> GetPipelines()
    {
        return memoryCache.Get<List<BuildDefinitionReference>>(PipelineKey) ?? [];
    }

    private void SetItem<T>(T item, string id)
    {
        if (!memoryCache.TryGetValue(id, out _))
        {
            memoryCache.Set(id, item);
        }
    }
}