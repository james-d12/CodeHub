namespace CodeHub.Platform.AzureDevOps.Models.Requests;

public sealed record AzureDevOpsQueryPipelineRequest
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public Guid? ProjectId { get; set; }
    public string? ProjectName { get; set; }
}