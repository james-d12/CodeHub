using System.Text.Json.Serialization;

namespace CodeHub.Engine.SonarCloud.Models;

[JsonSerializable(typeof(SonarCloudResponse<>))]
public sealed record SonarCloudResponse<T> where T: class
{
    [JsonPropertyName("paging")]
    public required SonarCloudPaging Paging { get; set; }
    
    [JsonPropertyName("components")]
    public required List<T> Components { get; set; }
}

[JsonSerializable(typeof(SonarCloudPaging))]
public sealed record SonarCloudPaging
{
    [JsonPropertyName("pageIndex")]
    public required int PageIndex { get; set; }
    [JsonPropertyName("pageSize")]
    public required int PageSize { get; set; }
    [JsonPropertyName("total")]
    public required int Total { get; set; }
}