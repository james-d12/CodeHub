using System.Text.Json.Serialization;

namespace CodeHub.Platform.Soos.Models;

[JsonSerializable(typeof(SoosProjectBranch))]
public sealed record SoosProjectBranch
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("hashId")]
    public required string HashId { get; init; }

    [JsonPropertyName("repositoryType")]
    public required string RepositoryType { get; init; }
}