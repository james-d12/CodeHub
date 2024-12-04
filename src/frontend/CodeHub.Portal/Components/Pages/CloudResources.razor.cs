using CodeHub.Platform.Azure.Models;
using CodeHub.Portal.Services.Services;
using MudBlazor;

namespace CodeHub.Portal.Components.Pages;

public partial class CloudResources
{
    private readonly IAzureHttpClient _azureHttpClient;

    private List<AzureResource>? _azureResources;
    private List<AzureSubscription> _azureSubscriptions = [];
    private HashSet<AzureSubscription> _selectedSubscriptions = [];
    private HashSet<AzureSubscription> _filterSubscriptions = [];
    private FilterDefinition<AzureSubscription>? _filterDefinition;
    private bool _filterOpen;
    private bool _selectAll = true;

    public CloudResources(IAzureHttpClient azureHttpClient)
    {
        _azureHttpClient = azureHttpClient;
    }

    protected override async Task OnInitializedAsync()
    {
        _azureSubscriptions = await _azureHttpClient.GetSubscriptionsAsync();
        _azureResources = await _azureHttpClient.GetResourcesAsync();
        _azureResources = [.. _azureResources.OrderBy(a => a.Name)];
        _filterDefinition = new FilterDefinition<AzureSubscription>
        {
            FilterFunction = x => _filterSubscriptions.Contains(x)
        };
    }

    private void OpenFilter()
    {
        _filterOpen = true;
    }

    private void SelectedChanged(bool value, AzureSubscription azureSubscription)
    {
        if (value)
            _selectedSubscriptions.Add(azureSubscription);
        else
            _selectedSubscriptions.Remove(azureSubscription);

        _selectAll = _selectedSubscriptions.Count == _azureSubscriptions.Count;
    }

    private async Task ClearFilterAsync(FilterContext<AzureSubscription> context)
    {
        _selectedSubscriptions = [.. _azureSubscriptions];
        _filterSubscriptions = [.. _azureSubscriptions];
        if (_filterDefinition != null) await context.Actions.ClearFilterAsync(_filterDefinition);
        _filterOpen = false;
    }

    private async Task ApplyFilterAsync(FilterContext<AzureSubscription> context)
    {
        _filterSubscriptions = [.. _selectedSubscriptions];
        if (_filterDefinition != null) await context.Actions.ApplyFilterAsync(_filterDefinition);
        _filterOpen = false;
    }

    private void SelectAll(bool value)
    {
        _selectAll = value;

        if (value)
        {
            _selectedSubscriptions = _azureSubscriptions.ToHashSet();
        }
        else
        {
            _selectedSubscriptions.Clear();
        }
    }
}