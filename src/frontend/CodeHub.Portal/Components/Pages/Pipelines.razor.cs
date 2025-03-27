using CodeHub.Domain.Git;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

public partial class Pipelines
{
    private readonly ILogger<Pipelines> _logger;
    private readonly IGitHttpClient _gitHttpClient;
    private List<Pipeline>? _pipelines;

    public Pipelines(ILogger<Pipelines> logger, IGitHttpClient gitHttpClient)
    {
        _logger = logger;
        _gitHttpClient = gitHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing Pipelines component");
        _pipelines = await _gitHttpClient.GetPipelinesAsync();
    }

    private static string GetPlatformIcon(Pipeline pipeline)
    {
        return pipeline.Platform switch
        {
            PipelinePlatform.AzureDevOps => PlatformIcons.AzureDevOps,
            PipelinePlatform.GitHub => PlatformIcons.GitHub,
            PipelinePlatform.GitLab => PlatformIcons.GitLab,
            _ => string.Empty
        };
    }
}