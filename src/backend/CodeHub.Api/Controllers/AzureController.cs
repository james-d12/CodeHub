using CodeHub.Core.Platforms.Azure;
using CodeHub.Core.Platforms.Azure.Models;
using CodeHub.Core.Platforms.Azure.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.Api.Controllers;

[ApiController]
[Route("azure/subscriptions")]
public sealed class AzureController(ILogger<AzureController> logger, IAzureService azureService)
    : ControllerBase
{
    [HttpGet, Route("")]
    public async Task<List<AzureSubscription>> GetSubscriptionsAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Subscription Resources for Azure.");
        return await azureService.GetSubscriptionsAsync(cancellationToken);
    }

    [HttpGet, Route("resources")]
    public async Task<List<AzureResource>> GetAllSubscriptionsResourcesAsync([FromQuery] string[] subscriptionIds,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Subscription Resources for Azure.");
        return await azureService.GetAllSubscriptionsResourcesAsync(subscriptionIds, cancellationToken);
    }

    [HttpGet, Route("{name}/resources")]
    public async Task<List<AzureResource>> GetResourcesForSubscriptionAsync(string name,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting All Subscription Resources for Azure.");
        return await azureService.GetSubscriptionResourcesAsync(name, cancellationToken);
    }
}