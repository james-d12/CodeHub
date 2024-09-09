using System.Text.Json.Serialization;

namespace CodeHub.Engine.ArgoCD.Models.Application;

[JsonSerializable(typeof(ApplicationItem))]
public sealed record ApplicationItem
{
    [JsonPropertyName("metadata")]
    public required ApplicationItemMetaData MetaData { get; set; }
}