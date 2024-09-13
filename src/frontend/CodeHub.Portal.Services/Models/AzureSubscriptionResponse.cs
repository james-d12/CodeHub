using System.Text.Json.Serialization;

namespace CodeHub.Portal.Services.Models;

[JsonSerializable(typeof(AzureSubscriptionResourceData))]
public record AzureSubscriptionResponse
{
    [JsonPropertyName("hasData")]
    public required bool HasData { get; init; }
    [JsonPropertyName("data")]
    public required AzureSubscriptionResourceData Data { get; init; }
}

[JsonSerializable(typeof(AzureSubscriptionResourceData))]
public record AzureSubscriptionResourceData
{
    [JsonPropertyName("displayName")]
    public required string Name { get; init; }  
}