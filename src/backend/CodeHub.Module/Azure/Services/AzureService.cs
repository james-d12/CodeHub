﻿using System.Collections.Concurrent;
using Azure.Identity;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using Azure.Security.KeyVault.Secrets;
using CodeHub.Domain.Cloud;
using CodeHub.Module.Azure.Extensions;
using CodeHub.Module.Azure.Models;
using CodeHub.Module.Shared.Extensions;

namespace CodeHub.Module.Azure.Services;

public sealed class AzureService : IAzureService
{
    private readonly ArmClient _client = new(new DefaultAzureCredential());

    public async Task<List<TenantResource>> GetTenantsAsync(CancellationToken cancellationToken)
    {
        var tenants = new List<TenantResource>();
        await foreach (var tenant in _client.GetTenants().GetAllAsync(cancellationToken))
        {
            tenants.Add(tenant);
        }

        return tenants;
    }

    public async Task<List<SubscriptionResource>> GetSubscriptionsAsync(CancellationToken cancellationToken)
    {
        var subscriptionResources = new List<SubscriptionResource>();

        await foreach (var subscription in _client.GetSubscriptions().GetAllAsync(cancellationToken))
        {
            subscriptionResources.Add(subscription);
        }

        return subscriptionResources;
    }

    public async Task<List<AzureCloudResource>> GetResourcesAsync(
        SubscriptionResource subscriptionResource,
        TenantResource tenantResource,
        CancellationToken cancellationToken)
    {
        var azureResources = new List<AzureCloudResource>();

        await foreach (var resource in subscriptionResource.GetGenericResourcesAsync(
                           cancellationToken: cancellationToken))
        {
            var azureResource = resource.Data.MapToAzureResource(
                tenantResource.Data.DisplayName,
                subscriptionResource.Data.DisplayName);
            azureResources.Add(azureResource);
        }

        return azureResources;
    }

    public async Task<List<CloudSecret>> GetKeyVaultSecretsAsync(List<AzureCloudResource> resources,
        CancellationToken cancellationToken)
    {
        var cloudSecrets = new ConcurrentBag<CloudSecret>();
        var vaults = resources.Where(r => r.Type.EqualsCaseInsensitive("vaults")).ToList();

        var tasks = vaults.Select(async vault =>
        {
            try
            {
                var vaultUri = new Uri($"https://{vault.Id.Value}.vault.azure.net/");
                var client = new SecretClient(vaultUri, new DefaultAzureCredential());

                await foreach (var secret in client.GetPropertiesOfSecretsAsync(cancellationToken))
                {
                    cloudSecrets.Add(new CloudSecret
                    {
                        Name = secret.Name,
                        Location = vault.Name,
                        Url = secret.VaultUri,
                        Platform = CloudSecretPlatform.Azure
                    });
                }
            }
            catch (Exception)
            {
                // Log exception if necessary
                // Log.Error(ex, "Error fetching secrets for vault: {VaultName}", vault.Name);
            }
        }).ToList();

        await Task.WhenAll(tasks);

        return cloudSecrets.ToList();
    }
}