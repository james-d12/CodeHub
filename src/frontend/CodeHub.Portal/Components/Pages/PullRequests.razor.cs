using CodeHub.Domain.Git;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

public partial class PullRequests
{
    private readonly ILogger<PullRequest> _logger;
    private readonly IGitHttpClient _gitHttpClient;
    private List<PullRequest>? _pullRequests;

    public PullRequests(ILogger<PullRequest> logger, IGitHttpClient gitHttpClient)
    {
        _logger = logger;
        _gitHttpClient = gitHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing Pull Requests component");
        _pullRequests = await _gitHttpClient.GetPullRequestsAsync();
    }

    private static string GetPlatformIcon(PullRequest pullRequest)
    {
        return pullRequest.Platform switch
        {
            PullRequestPlatform.AzureDevOps => PlatformIcons.AzureDevOps,
            PullRequestPlatform.GitLab => PlatformIcons.GitLab,
            PullRequestPlatform.GitHub => PlatformIcons.GitHub,
            _ => string.Empty
        };
    }
}