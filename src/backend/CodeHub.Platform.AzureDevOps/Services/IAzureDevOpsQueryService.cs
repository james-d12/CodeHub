using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Platform.AzureDevOps.Models.Requests;

namespace CodeHub.Platform.AzureDevOps.Services;

public interface IAzureDevOpsQueryService
{
    List<AzureDevOpsPipeline> QueryPipelines(AzureDevOpsQueryPipelineRequest request);
}