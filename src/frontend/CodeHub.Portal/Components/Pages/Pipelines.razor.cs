﻿using CodeHub.Domain.Git;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

public partial class Pipelines
{
    private readonly ILogger<Pipeline> _logger;
    private readonly IResourceHttpClient _resourceHttpClient;
    private List<Pipeline>? _pipelines;

    public Pipelines(ILogger<Pipeline> logger, IResourceHttpClient resourceHttpClient)
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