using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace CodeHub.Engine.AzureDevOps.Services;

internal interface IAzureDevOpsCacheService
{
    void SetRepositories(List<GitRepository> repositories);
    void SetPipelines(List<BuildDefinitionReference> pipelines);
    List<GitRepository> GetRepositories();
    List<BuildDefinitionReference> GetPipelines();
}