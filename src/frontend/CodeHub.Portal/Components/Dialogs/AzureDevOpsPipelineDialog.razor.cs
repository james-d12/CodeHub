using CodeHub.Module.AzureDevOps.Models;
using CodeHub.Portal.Services.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CodeHub.Portal.Components.Dialogs;

public partial class AzureDevOpsPipelineDialog
{
    [CascadingParameter]
    public required IMudDialogInstance MudDialog { get; set; }

    [Parameter]
    public required string RepositoryName { get; set; }

    private readonly IAzureDevOpsClient _azureDevOpsClient;

    private AzureDevOpsPipeline? _pipeline;

    public AzureDevOpsPipelineDialog(IAzureDevOpsClient azureDevOpsClient)
    {
        _azureDevOpsClient = azureDevOpsClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _pipeline = await _azureDevOpsClient.GetPipelineAsync(RepositoryName);
    }

    public void Submit() => MudDialog.Close(DialogResult.Ok(true));
}