using System.Text.Json.Serialization;

namespace CodeHub.Engine.ArgoCD.Models.Application;

[JsonSerializable(typeof(Application))]
public sealed record Application
{
    [JsonPropertyName("metadata")]
    public required ApplicationMetaData MetaData { get; set; }
    
    [JsonPropertyName("items")]
    public required ApplicationItem[] Items { get; set; }
}