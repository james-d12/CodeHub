using System.Text.Json.Serialization;

namespace CodeHub.Engine.ArgoCD.Models.Application;

[JsonSerializable(typeof(ApplicationItemMetaData))]
public sealed record ApplicationItemMetaData
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    
    [JsonPropertyName("namespace")]
    public required string Namespace { get; set; } 
}