using Azure.ResourceManager.Resources;

namespace CodeHub.Core.Platforms.Azure;

internal static class AzureSubscriptionMapper
{
    internal static AzureSubscription MapFromSubscriptionResource(SubscriptionResource subscriptionResource,
        string tenantName)
    {
        return new AzureSubscription
        {
            Id = subscriptionResource.Id.Name,
            Name = subscriptionResource.Data.DisplayName,
            Url = GetUrl(tenantName, subscriptionResource.Data),
            Tenant = tenantName,
            TenantId = subscriptionResource.Data.TenantId,
            Tags = subscriptionResource.Data.Tags
        };
    }

    private static string GetUrl(string tenantName, SubscriptionData subscriptionData)
    {
        return
            $"https://portal.azure.com/#@{tenantName}/resource/subscriptions/{subscriptionData.Id.SubscriptionId}/overview";
    }
}