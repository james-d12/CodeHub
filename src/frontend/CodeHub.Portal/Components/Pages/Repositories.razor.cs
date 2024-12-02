﻿using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

public partial class Repositories
{
    private readonly ILogger<Repositories> _logger;
    private readonly IAzureDevOpsHttpClient _azureDevOpsHttpClient;
    private List<AzureDevOpsRepository>? _azureDevOpsResources;

    public Repositories(ILogger<Repositories> logger, IAzureDevOpsHttpClient azureDevOpsHttpClient)
    {
        _logger = logger;
        _azureDevOpsHttpClient = azureDevOpsHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing GitResources component");
        _azureDevOpsResources = await _azureDevOpsHttpClient.GetRepositoriesAsync();
    }
}