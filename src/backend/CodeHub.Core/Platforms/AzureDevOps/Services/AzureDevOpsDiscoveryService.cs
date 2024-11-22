using CodeHub.Core.Services;
using Microsoft.Extensions.Logging;

namespace CodeHub.Core.Platforms.AzureDevOps.Services;

internal sealed class AzureDevOpsDiscoveryService : IDiscoveryService
{
    private readonly IAzureDevOpsService _azureDevOpsService;
    private readonly ILogger<AzureDevOpsDiscoveryService> _logger;

    public AzureDevOpsDiscoveryService(
        ILogger<AzureDevOpsDiscoveryService> logger,
        IAzureDevOpsService azureDevOpsService)
    {
        _logger = logger;
        _azureDevOpsService = azureDevOpsService;
    }

    public async Task DiscoverAsync(CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Discovering Azure DevOps team resources...");
            await _azureDevOpsService.GetTeamsAsync(cancellationToken);

            _logger.LogInformation("Discovering Azure DevOps project resources...");
            var projects = await _azureDevOpsService.GetProjectsAsync(cancellationToken);

            foreach (var project in projects)
            {
                _logger.LogInformation("Discovering Azure DevOps Repository resources for {ProjectName}", project.Name);
                await _azureDevOpsService.GetRepositoriesAsync(project.Name, cancellationToken);
                _logger.LogInformation("Discovering Azure DevOps Pipeline resources for {ProjectName}", project.Name);
                await _azureDevOpsService.GetPipelinesAsync(project.Name, cancellationToken);
                _logger.LogInformation("Discovering Azure DevOps Work Item resources for {ProjectName}", project.Name);
                await _azureDevOpsService.GetWorkItemsAsync(project.Name, cancellationToken);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error occurred whilst trying to discover the latest Azure DevOps resources.");
        }
    }
}