using CodeHub.Portal.Services.Services;
using CodeHub.Shared.Models;

namespace CodeHub.Portal.Components.Pages;

public partial class PullRequests
{
    private readonly ILogger<PullRequest> _logger;
    private readonly IResourceHttpClient _resourceHttpClient;
    private List<PullRequest>? _pullRequests;

    public PullRequests(ILogger<PullRequest> logger, IResourceHttpClient resourceHttpClient)
    {
        _logger = logger;
        _resourceHttpClient = resourceHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing Pull Requests component");
        _pullRequests = await _resourceHttpClient.GetPullRequests();
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