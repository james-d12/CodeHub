using System.Text.Json.Serialization;

namespace CodeHub.Core.Platforms.SonarCloud;

[JsonSerializable(typeof(SonarCloudResponse<>))]
public sealed record SonarCloudResponse<T> where T : class
{
    [JsonPropertyName("paging")]
    public required SonarCloudPaging Paging { get; init; }

    [JsonPropertyName("components")]
    public required List<T> Components { get; init; }
}

[JsonSerializable(typeof(SonarCloudPaging))]
public sealed record SonarCloudPaging
{
    [JsonPropertyName("pageIndex")]
    public required int PageIndex { get; init; }

    [JsonPropertyName("pageSize")]
    public required int PageSize { get; init; }

    [JsonPropertyName("total")]
    public required int Total { get; init; }
}