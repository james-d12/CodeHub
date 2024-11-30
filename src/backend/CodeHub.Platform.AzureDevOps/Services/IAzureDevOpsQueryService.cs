using CodeHub.Platform.AzureDevOps.Models;

namespace CodeHub.Platform.AzureDevOps.Services;

public interface IAzureDevOpsQueryService
{
    List<AzureDevOpsPipeline> QueryPipelines();
}