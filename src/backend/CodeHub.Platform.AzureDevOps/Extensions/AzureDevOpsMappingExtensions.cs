using System.Collections.Frozen;
using System.Collections.Immutable;
using CodeHub.Shared.Models;
using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;

namespace CodeHub.Platform.AzureDevOps.Extensions;

internal static class AzureDevOpsMappingExtensions
{
    internal static AzureDevOpsPipeline MapToAzureDevOpsPipeline(this BuildDefinitionReference buildDefinitionReference)
    {
        return new AzureDevOpsPipeline
        {
            Id = buildDefinitionReference.Id.ToString(),
            Name = buildDefinitionReference.Name,
            Url = new Uri(buildDefinitionReference.Url),
            Project = buildDefinitionReference.Project.Name,
            ProjectName = buildDefinitionReference.Project.Url,
            Path = buildDefinitionReference.Path,
            Platform = PipelinePlatform.AzureDevOps
        };
    }

    internal static AzureDevOpsProject MapToAzureDevOpsProject(this TeamProjectReference teamProjectReference)
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

    internal static AzureDevOpsRepository MapToAzureDevOpsRepository(this GitRepository gitRepository)
    {
        return new AzureDevOpsRepository
        {
            Id = gitRepository.Id.ToString(),
            Name = gitRepository.Name,
            Url = new Uri(gitRepository.WebUrl),
            DefaultBranch = gitRepository.DefaultBranch,
            Project = gitRepository.ProjectReference.Name,
            ProjectUrl = gitRepository.ProjectReference.Url,
            IsDisabled = gitRepository.IsDisabled ?? false,
            IsInMaintenance = gitRepository.IsInMaintenance ?? false,
            Platform = GitPlatform.AzureDevOps
        };
    }

    internal static AzureDevOpsTeam MapToAzureDevOpsTeam(this WebApiTeam webApiTeam)
    {
        return new AzureDevOpsTeam
        {
            Id = webApiTeam.Id,
            Name = webApiTeam.Name,
            Description = webApiTeam.Description,
            Url = webApiTeam.Url
        };
    }

    internal static AzureDevOpsPullRequest MapToAzureDevOpsPullRequest(this GitPullRequest gitPullRequest)
    {
        AzureDevOpsPullRequestStatus status = gitPullRequest.Status switch
        {
            PullRequestStatus.NotSet => AzureDevOpsPullRequestStatus.NotSet,
            PullRequestStatus.Active => AzureDevOpsPullRequestStatus.Active,
            PullRequestStatus.Abandoned => AzureDevOpsPullRequestStatus.NotSet,
            PullRequestStatus.Completed => AzureDevOpsPullRequestStatus.NotSet,
            PullRequestStatus.All => AzureDevOpsPullRequestStatus.NotSet,
            _ => AzureDevOpsPullRequestStatus.NotSet
        };

        return new AzureDevOpsPullRequest
        {
            Id = gitPullRequest.PullRequestId,
            Title = gitPullRequest.Title,
            Description = gitPullRequest.Description,
            Labels = gitPullRequest.Labels.Select(l => l.Name).ToImmutableHashSet() ?? [],
            Reviewers = gitPullRequest.Reviewers.Select(r => r.DisplayName).ToImmutableHashSet() ?? [],
            Status = status
        };
    }

    internal static AzureDevOpsWorkItem MapToAzureDevOpsWorkItem(this WorkItem workItem)
    {
        return new AzureDevOpsWorkItem
        {
            Id = workItem.Id ?? 0,
            Url = workItem.Url,
            Revision = workItem.Rev ?? 0,
            Fields = workItem.Fields?.ToFrozenDictionary() ?? FrozenDictionary<string, object>.Empty,
            Relations = workItem.Relations.Select(r => r.Title).ToImmutableHashSet() ?? []
        };
    }
}