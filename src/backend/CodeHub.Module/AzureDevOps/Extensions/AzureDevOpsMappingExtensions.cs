﻿using System.Collections.Frozen;
using System.Collections.Immutable;
using CodeHub.Domain.Git;
using CodeHub.Domain.Ticketing;
using CodeHub.Module.AzureDevOps.Models;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using PullRequestStatus = Microsoft.TeamFoundation.SourceControl.WebApi.PullRequestStatus;
using WorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

namespace CodeHub.Module.AzureDevOps.Extensions;

public static class AzureDevOpsMappingExtensions
{
    public static AzureDevOpsPipeline MapToAzureDevOpsPipeline(this BuildDefinitionReference buildDefinitionReference)
    {
        return new AzureDevOpsPipeline
        {
            Id = new PipelineId(buildDefinitionReference.Id.ToString()),
            Name = buildDefinitionReference.Name,
            Url = new Uri(buildDefinitionReference.Url),
            Path = buildDefinitionReference.Path,
            Platform = PipelinePlatform.AzureDevOps,
            Owner = new Owner
            {
                Id = new OwnerId(buildDefinitionReference.Project.Id.ToString()),
                Name = buildDefinitionReference.Project.Name,
                Description = buildDefinitionReference.Project.Description,
                Url = new Uri(buildDefinitionReference.Project.Url.Replace("_apis/", string.Empty)),
                Platform = OwnerPlatform.AzureDevOps,
            }
        };
    }

    public static AzureDevOpsProject MapToAzureDevOpsProject(this TeamProjectReference teamProjectReference)
    {
        var teamProject = new TeamProject(teamProjectReference);

        return new AzureDevOpsProject
        {
            Id = teamProjectReference.Id,
            Name = teamProject.Name,
            Description = teamProject.Description,
            Url = new Uri(teamProject.Url),
        };
    }

    public static AzureDevOpsRepository MapToAzureDevOpsRepository(this GitRepository gitRepository)
    {
        return new AzureDevOpsRepository
        {
            Id = new RepositoryId(gitRepository.Id.ToString()),
            Name = gitRepository.Name,
            Url = new Uri(gitRepository.WebUrl),
            DefaultBranch = gitRepository.DefaultBranch?.Replace("refs/heads/", string.Empty) ?? string.Empty,
            IsDisabled = gitRepository.IsDisabled ?? false,
            IsInMaintenance = gitRepository.IsInMaintenance ?? false,
            Platform = RepositoryPlatform.AzureDevOps,
            Owner = new Owner
            {
                Id = new OwnerId(gitRepository.ProjectReference.Id.ToString()),
                Name = gitRepository.ProjectReference.Name,
                Description = gitRepository.ProjectReference.Description,
                Url = new Uri(gitRepository.ProjectReference.Url.Replace("_apis/", string.Empty)),
                Platform = OwnerPlatform.AzureDevOps,
            }
        };
    }

    public static AzureDevOpsTeam MapToAzureDevOpsTeam(this WebApiTeam webApiTeam)
    {
        return new AzureDevOpsTeam
        {
            Id = webApiTeam.Id,
            Name = webApiTeam.Name,
            Description = webApiTeam.Description,
            Url = webApiTeam.Url
        };
    }

    public static AzureDevOpsPullRequest MapToAzureDevOpsPullRequest(this GitPullRequest gitPullRequest)
    {
        var status = gitPullRequest.Status switch
        {
            PullRequestStatus.NotSet => Domain.Git.PullRequestStatus.Draft,
            PullRequestStatus.Active => Domain.Git.PullRequestStatus.Active,
            PullRequestStatus.Abandoned => Domain.Git.PullRequestStatus.Abandoned,
            PullRequestStatus.Completed => Domain.Git.PullRequestStatus.Completed,
            PullRequestStatus.All => Domain.Git.PullRequestStatus.Unknown,
            _ => Domain.Git.PullRequestStatus.Unknown
        };

        return new AzureDevOpsPullRequest
        {
            Id = new PullRequestId(gitPullRequest.PullRequestId.ToString()),
            Name = gitPullRequest.Title,
            Description = gitPullRequest.Description,
            Url = new Uri(gitPullRequest.Url),
            Labels = gitPullRequest.Labels?.Select(l => l.Name).ToImmutableHashSet() ?? [],
            Reviewers = gitPullRequest.Reviewers?.Select(r => r.DisplayName).ToImmutableHashSet() ?? [],
            Status = status,
            Platform = PullRequestPlatform.AzureDevOps,
            LastCommit = new Commit
            {
                Id = new CommitId(gitPullRequest.LastMergeCommit?.CommitId ?? string.Empty),
                Url = new Uri(gitPullRequest.LastMergeCommit?.Url ?? "https://dev.azure.com"),
                Committer = gitPullRequest.LastMergeCommit?.Committer?.Name ?? string.Empty,
                Comment = gitPullRequest.LastMergeCommit?.Comment ?? string.Empty,
                ChangeCount = gitPullRequest.LastMergeCommit?.ChangeCounts?.Count ?? 0
            },
            RepositoryName = gitPullRequest.Repository?.Name ?? string.Empty,
            RepositoryUrl = new Uri(gitPullRequest.Repository?.Url ?? string.Empty),
            CreatedOnDate = DateOnly.FromDateTime(gitPullRequest.CreationDate),
        };
    }

    public static AzureDevOpsWorkItem MapToAzureDevOpsWorkItem(this WorkItem workItem)
    {
        return new AzureDevOpsWorkItem
        {
            Id = new WorkItemId(workItem.Id?.ToString() ?? string.Empty),
            Title = workItem.Fields["System.Title"]?.ToString() ?? string.Empty,
            Description = string.Empty,
            Type = workItem.Fields["System.WorkItemType"]?.ToString() ?? string.Empty,
            State = workItem.Fields["System.State"]?.ToString() ?? string.Empty,
            Url = workItem.Url,
            Revision = workItem.Rev ?? 0,
            Fields = workItem.Fields?.ToFrozenDictionary() ?? FrozenDictionary<string, object>.Empty,
            Relations = workItem.Relations?.Select(r => r.Title)
                            .ToImmutableHashSet() ??
                        [],
            Platform = WorkItemPlatform.AzureDevOps
        };
    }
}