using Azure.ResourceManager.Resources;

namespace CodeHub.Engine.Azure.Models;

public sealed record AzureSubscription
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required string Tenant { get; init; }
    public required Guid? TenantId { get; init; }
    public required IReadOnlyDictionary<string, string> Tags { get; init; }

    public static AzureSubscription MapFromSubscriptionResource(SubscriptionResource subscriptionResource,
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