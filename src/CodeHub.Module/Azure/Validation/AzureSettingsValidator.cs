using CodeHub.Module.Azure.Models;
using CodeHub.Module.Shared.Validation;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Module.Azure.Validation;

public static class AzureSettingsValidator
{
    public static AzureSettings GetValidSettings(IConfiguration configuration)
    {
        return new ValidationBuilder<AzureSettings>(configuration)
            .SectionExists(nameof(AzureSettings))
            .CheckEnabled(x => x.IsEnabled, nameof(AzureSettings.IsEnabled))
            .CheckValue(x => x.SubscriptionFilters, nameof(AzureSettings.SubscriptionFilters))
            .Build();
    }
}