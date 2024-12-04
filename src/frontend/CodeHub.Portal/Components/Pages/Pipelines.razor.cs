using CodeHub.Portal.Services.Services;
using CodeHub.Shared.Models;

namespace CodeHub.Portal.Components.Pages;

public partial class Pipelines
{
    private readonly ILogger<Repositories> _logger;
    private readonly IResourceHttpClient _resourceHttpClient;
    private List<Pipeline>? _pipelines;

    public Pipelines(ILogger<Repositories> logger, IResourceHttpClient resourceHttpClient)
    {
        _logger = logger;
        _resourceHttpClient = resourceHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing Pipelines component");
        _pipelines = await _resourceHttpClient.GetPipelines();
    }

    private static string GetPlatformIcon(Pipeline pipeline)
    {
        return pipeline.Platform switch
        {
            PipelinePlatform.AzureDevOps => PlatformIcons.AzureDevOps,
            _ => string.Empty
        };
    }
}