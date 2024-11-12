using Azure.ResourceManager.Resources;
using CodeHub.Core.Platforms.Azure.Models;

namespace CodeHub.Core.Platforms.Azure.Extensions;

internal static class AzureMappingExtensions
{
    internal static AzureResource MapToAzureResource(
        this GenericResourceData genericResourceData,
        string tenantName,
        string subscriptionName)
    {
        return new AzureResource
        {
            Id = genericResourceData.Id.Name,
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

    internal static AzureSubscription MapToAzureSubscription(
        this SubscriptionResource subscriptionResource,
        string tenantName)
    {
        return new AzureSubscription
        {
            Id = subscriptionResource.Id.Name,
            Name = subscriptionResource.Data.DisplayName,
            Url = GetSubscriptionUrl(tenantName, subscriptionResource.Data.SubscriptionId),
            Tenant = tenantName,
            TenantId = subscriptionResource.Data.TenantId,
            Tags = subscriptionResource.Data.Tags
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