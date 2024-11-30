using CodeHub.Shared.Models;

namespace CodeHub.Shared.Services;

public interface IPipelineQueryService
{
    List<Pipeline> QueryPipelines();
}