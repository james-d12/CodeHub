using CodeHub.Domain.Git;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

public partial class Repositories
{
    private readonly ILogger<Repositories> _logger;
    private readonly IGitHttpClient _gitHttpClient;
    private List<Repository>? _repositories;

    public Repositories(ILogger<Repositories> logger, IGitHttpClient gitHttpClient)
    {
        _logger = logger;
        _gitHttpClient = gitHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing Repositories component");
        _repositories = await _gitHttpClient.GetRepositoriesAsync();
    }

    private static string GetPlatformIcon(Repository repository)
    {
        return repository.Platform switch
        {
            RepositoryPlatform.AzureDevOps => PlatformIcons.AzureDevOps,
            RepositoryPlatform.GitHub => PlatformIcons.GitHub,
            RepositoryPlatform.GitLab => PlatformIcons.GitLab,
            _ => string.Empty
        };
    }
}