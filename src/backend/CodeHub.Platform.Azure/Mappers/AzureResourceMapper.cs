using Azure.ResourceManager.Resources;
using CodeHub.Platform.Azure.Models;

namespace CodeHub.Platform.Azure.Mappers;

internal static class AzureResourceMapper
{
    internal static AzureResource MapFromGenericResource(GenericResourceData genericResourceData, string tenantName,
        string subscriptionName)
    {
        return new AzureResource
        {
            Id = genericResourceData.Id.Name,
            Name = genericResourceData.Name,
            TenantName = tenantName,
            Kind = genericResourceData.Kind,
            Subscription = subscriptionName,
            SubscriptionId = genericResourceData.Id.SubscriptionId,
            ResourceGroupName = genericResourceData.Id.ResourceGroupName,
            ResourceType = genericResourceData.Id.ResourceType,
            Url = GetUrl(tenantName, genericResourceData),
            Location = GetLocationName(genericResourceData),
            ResourceGroupUrl = GetResourceGroupUrl(tenantName, genericResourceData),
            SubscriptionUrl = GetSubscriptionUrl(tenantName, genericResourceData),
        };
    }

    private static string GetLocationName(GenericResourceData genericResourceData)
    {
        var locationName = genericResourceData.Location.DisplayName;
        return string.IsNullOrEmpty(locationName) ? "Global" : locationName;
    }

    private static string GetUrl(string tenantName, GenericResourceData genericResourceData)
    {
        return
            $"https://portal.azure.com/#@{tenantName}/resource/subscriptions/{genericResourceData.Id.SubscriptionId}/resourceGroups/{genericResourceData.Id.ResourceGroupName}/providers/{genericResourceData.Id.ResourceType}/{genericResourceData.Name}";
    }

    private static string GetSubscriptionUrl(string tenantName, GenericResourceData genericResourceData)
    {
        return
            $"https://portal.azure.com/#@{tenantName}/resource/subscriptions/{genericResourceData.Id.SubscriptionId}/overview";
    }

    private static string GetResourceGroupUrl(string tenantName, GenericResourceData genericResourceData)
    {
        return
            $"https://portal.azure.com/#@{tenantName}/resource/subscriptions/{genericResourceData.Id.SubscriptionId}/resourceGroups/{genericResourceData.Id.ResourceGroupName}/overview";
    }
}