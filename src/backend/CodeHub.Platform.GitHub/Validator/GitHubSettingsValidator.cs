﻿using CodeHub.Platform.GitHub.Models;
using CodeHub.Shared.Validation;
using Microsoft.Extensions.Configuration;

namespace CodeHub.Platform.GitHub.Validator;

internal static class GitHubSettingsValidator
{
    internal static GitHubSettings GetValidSettings(IConfiguration configuration)
    {
        return new ValidationBuilder<GitHubSettings>(configuration)
            .SectionExists(nameof(GitHubSettings))
            .CheckEnabled(x => x.IsEnabled, nameof(GitHubSettings.IsEnabled))
            .CheckValue(x => x.AgentName, nameof(GitHubSettings.AgentName))
            .CheckValue(x => x.Token, nameof(GitHubSettings.Token))
            .Build();
    }
}