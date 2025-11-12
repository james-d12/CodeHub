using CodeHub.Module.Shared;

namespace CodeHub.Module.Azure.Models;

public sealed class AzureSettings : Settings
{
    public List<string> SubscriptionFilters { get; set; } = [];
}