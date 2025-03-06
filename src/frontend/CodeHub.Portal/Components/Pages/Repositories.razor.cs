using CodeHub.Core.Shared.Models;
using CodeHub.Portal.Services.Services;

namespace CodeHub.Portal.Components.Pages;

public partial class Repositories
{
    private readonly ILogger<Repositories> _logger;
    private readonly IResourceHttpClient _resourceHttpClient;
    private List<Repository>? _repositories;

    public Repositories(ILogger<Repositories> logger, IResourceHttpClient resourceHttpClient)
    {
        _logger = logger;
        _resourceHttpClient = resourceHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _logger.LogInformation("Initializing Repositories component");
        _repositories = await _resourceHttpClient.GetRepositories();
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