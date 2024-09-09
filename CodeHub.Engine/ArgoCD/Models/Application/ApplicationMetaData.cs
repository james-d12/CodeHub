using System.Text.Json.Serialization;

namespace CodeHub.Engine.ArgoCD.Models.Application;

[JsonSerializable(typeof(ApplicationMetaData))]
public sealed record ApplicationMetaData
{
    [JsonPropertyName("resourceVersion")]
    public required string ResourceVersion { get; set; }
}