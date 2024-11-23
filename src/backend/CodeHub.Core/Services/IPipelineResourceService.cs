using CodeHub.Core.Models.Resource;

namespace CodeHub.Core.Services;

public interface IPipelineResourceService
{
    Task<List<PipelineResource>> GetPipelineResourcesAsync(CancellationToken cancellationToken);
    Task<PipelineResource?> GetPipelineResourceAsync(string id, CancellationToken cancellationToken);
}