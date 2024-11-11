using CodeHub.Core.Models.Resource;

namespace CodeHub.Core.Platforms.Azure;

public sealed record AzureResource : CloudResource
{
    public required string TenantName { get; init; }
    public required string Kind { get; init; }
    public required string Subscription { get; init; }
    public required string? SubscriptionId { get; set; }
    public required Uri? SubscriptionUrl { get; init; }
    public required string? ResourceGroupName { get; init; }
    public required Uri? ResourceGroupUrl { get; init; }
    public required string? ResourceType { get; init; }
    public required string? Location { get; init; }
}