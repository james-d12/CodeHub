namespace CodeHub.Shared.Models.Requests;

public record QueryPipelineRequest
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public PipelinePlatform? Platform { get; set; }
}