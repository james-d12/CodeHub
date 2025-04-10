using System.Text.Json;
using CodeHub.Domain.Git;
using CodeHub.Portal.Features.Git.AzureDevOps;
using CodeHub.Portal.Features.Git.Client;
using CodeHub.Portal.Features.Shared;
using MudBlazor;

namespace CodeHub.Portal.Features.Git;

public partial class Repositories
{
    private readonly ILogger<Repositories> _logger;
    private readonly IGitHttpClient _gitHttpClient;
    private readonly IDialogService _dialogService;
    private List<Repository>? _repositories;

    public Repositories(ILogger<Repositories> logger, IGitHttpClient gitHttpClient, IDialogService dialogService)
    {
        _logger = logger;
        _gitHttpClient = gitHttpClient;
        _dialogService = dialogService;
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

    private Task OnRowClick(DataGridRowClickEventArgs<Repository> clickedRepository)
    {
        _logger.LogInformation("Row has been clicked with data: {Data}.",
            JsonSerializer.Serialize(clickedRepository.Item));

        var parameters = new DialogParameters
        {
            { nameof(AzureDevOpsRepositoryDialog.RepositoryName), clickedRepository.Item.Name }
        };

        var options = new DialogOptions { CloseOnEscapeKey = true };

        return _dialogService.ShowAsync<AzureDevOpsRepositoryDialog>("Simple Dialog", parameters, options);
    }
}