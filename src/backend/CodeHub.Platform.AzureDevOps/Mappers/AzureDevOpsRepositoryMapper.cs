﻿using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace CodeHub.Platform.AzureDevOps.Mappers;

internal static class AzureDevOpsRepositoryMapper
{
    internal static AzureDevOpsRepository MapFromGitRepository(GitRepository gitRepository)
    {
        return new AzureDevOpsRepository
        {
            Name = gitRepository.Name,
            Url = gitRepository.WebUrl,
            DefaultBranch = gitRepository.DefaultBranch,
            Project = gitRepository.ProjectReference.Name,
            ProjectUrl = gitRepository.ProjectReference.Url,
            IsDisabled = gitRepository.IsDisabled ?? false,
            IsInMaintenance = gitRepository.IsInMaintenance ?? false
        };
    }
}