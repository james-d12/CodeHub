using CodeHub.Core.Platforms.AzureDevOps.Models;
using Microsoft.Extensions.Options;

namespace CodeHub.Core.Platforms.AzureDevOps.Validation;

internal sealed class AzureDevOpsSettingsValidation : IValidateOptions<AzureDevOpsSettings>
{
    public ValidateOptionsResult Validate(string? name, AzureDevOpsSettings options)
    {
        if (string.IsNullOrEmpty(options.Organization))
        {
            return ValidateOptionsResult.Fail("Azure DevOps organization must not be null or empty.");
        }

        if (string.IsNullOrEmpty(options.PersonalAccessToken))
        {
            return ValidateOptionsResult.Fail("Azure DevOps personal access token must not be null or empty.");
        }

        return ValidateOptionsResult.Success;
    }
}