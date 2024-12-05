namespace CodeHub.Shared.Models.Requests;

public class QueryPullRequestRequest
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public PullRequestPlatform? Platform { get; set; }
}