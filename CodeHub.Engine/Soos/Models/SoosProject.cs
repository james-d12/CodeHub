using System.Text.Json.Serialization;

namespace CodeHub.Engine.SooS.Models;

[JsonSerializable(typeof(SoosProject))]
public sealed record SoosProject
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}