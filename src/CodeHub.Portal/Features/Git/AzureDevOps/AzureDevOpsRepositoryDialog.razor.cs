using CodeHub.Module.AzureDevOps.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CodeHub.Portal.Features.Git.AzureDevOps;

public partial class AzureDevOpsRepositoryDialog
{
    [CascadingParameter]
    public required IMudDialogInstance MudDialog { get; set; }

    [Parameter]
    public required string RepositoryName { get; set; }

    private readonly IAzureDevOpsClient _azureDevOpsClient;

    private AzureDevOpsRepository? _repository;

    public AzureDevOpsRepositoryDialog(IAzureDevOpsClient azureDevOpsClient)
    {
        _azureDevOpsClient = azureDevOpsClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _repository = await _azureDevOpsClient.GetRepositoryAsync(RepositoryName);
    }

    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
}