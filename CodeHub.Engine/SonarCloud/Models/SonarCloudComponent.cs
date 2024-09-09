using System.Text.Json.Serialization;

namespace CodeHub.Engine.SonarCloud.Models;

[JsonSerializable(typeof(SonarCloudComponent))]
public sealed record SonarCloudComponent
{
    [JsonPropertyName("organization")] public required string Organization { get; init; }

    [JsonPropertyName("key")] public required string Key { get; init; }

    [JsonPropertyName("name")] public required string Name { get; init; }

    [JsonPropertyName("qualifier")] public required string Qualifier { get; init; }

    [JsonPropertyName("project")] public required string Project { get; init; }

    public string GetUrl()
    {
        return $"https://sonarcloud.io/project/overview?id={Name}";
    }
}