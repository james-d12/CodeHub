﻿using Azure.Core;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using CodeHub.Platform.Azure.Mappers;
using CodeHub.Platform.Azure.Models;

namespace CodeHub.Platform.Azure.Services;

internal sealed class AzureService(IAzureCacheService azureCacheService) : IAzureService
{
    private readonly ArmClient _client = new(new DefaultAzureCredential());

    public async Task<TenantResource?> GetTenantAsync(string id, CancellationToken cancellationToken)
    {
        var tenants = await GetTenantsAsync(cancellationToken);
        return tenants.Find(ten => ten.Data.DisplayName == id || ten.Data.TenantId.ToString() == id);
    }

    public async Task<List<TenantResource>> GetTenantsAsync(CancellationToken cancellationToken)
    {
        var cachedTenants = azureCacheService.GetTenants();

        if (cachedTenants.Count >= 1)
        {
            return cachedTenants;
        }

        var tenants = new List<TenantResource>();

        await foreach (var tenant in _client.GetTenants().GetAllAsync(cancellationToken))
        {
            tenants.Add(tenant);
        }

        azureCacheService.SetTenants(tenants);

        return tenants;
    }

    public async Task<AzureSubscription?> GetSubscriptionAsync(string id, CancellationToken cancellationToken)
    {        
        var subscriptions = await GetSubscriptionsAsync(cancellationToken);
        return subscriptions.Find(sub => sub.Name == id || sub.Id == id);
    }

    public async Task<List<AzureSubscription>> GetSubscriptionsAsync(CancellationToken cancellationToken)
    {
        var subscriptionResources = await GetSubscriptionsResourceAsync(cancellationToken);

        var azureSubscriptions = new List<AzureSubscription>();

        foreach (var subscriptionResource in subscriptionResources)
        {
            var tenantId = subscriptionResource.Data.TenantId.ToString() ?? string.Empty;
            var tenant = await GetTenantAsync(tenantId, cancellationToken);
            var tenantName = tenant?.Data.DefaultDomain ?? string.Empty;
            var azureSubscription =
                AzureSubscriptionMapper.MapFromSubscriptionResource(subscriptionResource, tenantName);
            azureSubscriptions.Add(azureSubscription);
        }

        return azureSubscriptions;
    }

    public async Task<List<AzureResource>> GetSubscriptionResourcesAsync(string subscriptionId,
        CancellationToken cancellationToken)
    {
        var cachedAzureResources = azureCacheService.GetResources(subscriptionId);

        if (cachedAzureResources.Count >= 1)
        {
            return cachedAzureResources;
        }

        var subscriptionResource = await GetSubscriptionResourceAsync(subscriptionId, cancellationToken);

        if (subscriptionResource is null)
        {
            return [];
        }

        var tenantId = subscriptionResource.Data.TenantId.ToString() ?? string.Empty;
        var tenant = await GetTenantAsync(tenantId, cancellationToken);
        var tenantName = tenant?.Data.DefaultDomain ?? string.Empty;

        var azureResources = new List<AzureResource>();

        await foreach (var resource in subscriptionResource.GetGenericResourcesAsync(
                           cancellationToken: cancellationToken))
        {
            var azureResource = AzureResourceMapper.MapFromGenericResource(
                resource.Data,
                tenantName,
                subscriptionResource.Data.DisplayName);
            azureResources.Add(azureResource);
        }

        azureCacheService.SetResources(azureResources, subscriptionResource.Id.Name);

        return azureResources;
    }

    public async Task<List<AzureResource>> GetAllSubscriptionsResourcesAsync(string[] subscriptionIds,
        CancellationToken cancellationToken)
    {
        var resources = new List<AzureResource>();

        foreach (var subscriptionId in subscriptionIds)
        {
            var subscriptionResources = await GetSubscriptionResourcesAsync(subscriptionId, cancellationToken);
            resources.AddRange(subscriptionResources);
        }

        return resources;
    }


    private async Task<SubscriptionResource?> GetSubscriptionResourceAsync(string id,
        CancellationToken cancellationToken)
    {
        var subscriptionResource = _client.GetSubscriptionResource(new ResourceIdentifier(id));
        
        var subscriptions = await GetSubscriptionsResourceAsync(cancellationToken);
        return subscriptions.Find(sub => sub.Data.DisplayName == id || sub.Data.Id.Name == id);
    }

    private async Task<List<SubscriptionResource>> GetSubscriptionsResourceAsync(CancellationToken cancellationToken)
    {
        var cachedSubscriptionResources = azureCacheService.GetSubscriptions();

        if (cachedSubscriptionResources.Count >= 1)
        {
            return cachedSubscriptionResources;
        }

        var subscriptionResources = new List<SubscriptionResource>();

        await foreach (var subscription in _client.GetSubscriptions().GetAllAsync(cancellationToken))
        {
            subscriptionResources.Add(subscription);
        }

        azureCacheService.SetSubscriptions(subscriptionResources);

        return subscriptionResources;
    }
}