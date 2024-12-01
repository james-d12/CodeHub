namespace CodeHub.Shared.Models.Requests;

public record QueryRepositoryRequest
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? DefaultBranch { get; set; }
    public GitPlatform? Platform { get; set; }
}