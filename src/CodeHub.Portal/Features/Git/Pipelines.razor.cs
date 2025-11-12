using System.Text.Json;
using CodeHub.Domain.Git;
using CodeHub.Portal.Features.Git.AzureDevOps;
using CodeHub.Portal.Features.Git.Client;
using CodeHub.Portal.Features.Shared;
using MudBlazor;

namespace CodeHub.Portal.Features.Git;

public partial class Pipelines
{
    private readonly ILogger<Pipelines> _logger;
    private readonly IGitHttpClient _gitHttpClient;
    private readonly IDialogService _dialogService;
    private List<Pipeline>? _pipelines;

    public Pipelines(ILogger<Pipelines> logger, IGitHttpClient gitHttpClient, IDialogService dialogService)
    {
        _logger = logger;
        _gitHttpClient = gitHttpClient;
        _dialogService = dialogService;
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

    private Task OnRowClick(DataGridRowClickEventArgs<Pipeline> clickedPipeline)
    {
        _logger.LogInformation("Row has been clicked with data: {Data}.",
            JsonSerializer.Serialize(clickedPipeline.Item));

        var parameters = new DialogParameters
        {
            { nameof(AzureDevOpsRepositoryDialog.RepositoryName), clickedPipeline.Item.Name }
        };

        var options = new DialogOptions { CloseOnEscapeKey = true };

        return _dialogService.ShowAsync<AzureDevOpsPipelineDialog>("Simple Dialog", parameters, options);
    }
}