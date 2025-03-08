using Azure.ResourceManager.Resources;
using CodeHub.Module.Azure.Models;
using CodeHub.Domain.Cloud;

namespace CodeHub.Module.Azure.Extensions;

public static class AzureMappingExtensions
{
    public static AzureCloudResource MapToAzureResource(
        this GenericResourceData genericResourceData,
        string tenantName,
        string subscriptionName)
    {
        return new AzureCloudResource
        {
            Id = new CloudResourceId(genericResourceData.Id.Name),
            Platform = CloudPlatform.Azure,
            Name = genericResourceData.Name,
            Description = string.Empty,
            TenantName = tenantName,
            Kind = genericResourceData.Kind,
            Subscription = subscriptionName,
            SubscriptionId = genericResourceData.Id.SubscriptionId,
            ResourceGroupName = genericResourceData.Id.ResourceGroupName,
            ResourceType = genericResourceData.Id.ResourceType,
            Url = GetUrl(tenantName, genericResourceData),
            Location = GetLocationName(genericResourceData),
            ResourceGroupUrl = GetResourceGroupUrl(tenantName, genericResourceData),
            SubscriptionUrl = GetSubscriptionUrl(tenantName, genericResourceData.Id.SubscriptionId ?? string.Empty),
        };
    }

    private static string GetLocationName(GenericResourceData genericResourceData)
    {
        var locationName = genericResourceData.Location.DisplayName;
        return string.IsNullOrEmpty(locationName) ? "Global" : locationName;
    }

    private static Uri GetUrl(string tenantName, GenericResourceData genericResourceData)
    {
        return new Uri(
            $"https://portal.azure.com/#@{tenantName}/resource/subscriptions/{genericResourceData.Id.SubscriptionId}/resourceGroups/{genericResourceData.Id.ResourceGroupName}/providers/{genericResourceData.Id.ResourceType}/{genericResourceData.Name}");
    }

    private static Uri GetSubscriptionUrl(string tenantName, string subscriptionId)
    {
        return new Uri(
            $"https://portal.azure.com/#@{tenantName}/resource/subscriptions/{subscriptionId}/overview");
    }

    private static Uri GetResourceGroupUrl(string tenantName, GenericResourceData genericResourceData)
    {
        return new Uri(
            $"https://portal.azure.com/#@{tenantName}/resource/subscriptions/{genericResourceData.Id.SubscriptionId}/resourceGroups/{genericResourceData.Id.ResourceGroupName}/overview");
    }
}