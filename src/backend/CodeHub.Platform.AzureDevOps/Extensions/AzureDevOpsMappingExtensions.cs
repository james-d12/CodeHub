using System.Collections.Frozen;
using System.Collections.Immutable;
using CodeHub.Platform.AzureDevOps.Models;
using CodeHub.Shared.Models;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using PullRequestStatus = Microsoft.TeamFoundation.SourceControl.WebApi.PullRequestStatus;

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
            Path = buildDefinitionReference.Path,
            Platform = PipelinePlatform.AzureDevOps,
            Owner = new Owner
            {
                Id = buildDefinitionReference.Project.Id.ToString(),
                Name = buildDefinitionReference.Project.Name,
                Description = buildDefinitionReference.Project.Description,
                Url = new Uri(buildDefinitionReference.Project.Url.Replace("_apis/", string.Empty)),
                Platform = OwnerPlatform.AzureDevOps,
            }
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
            DefaultBranch = gitRepository.DefaultBranch?.Replace("refs/heads/", string.Empty) ?? string.Empty,
            IsDisabled = gitRepository.IsDisabled ?? false,
            IsInMaintenance = gitRepository.IsInMaintenance ?? false,
            Platform = RepositoryPlatform.AzureDevOps,
            Owner = new Owner
            {
                Id = gitRepository.ProjectReference.Id.ToString(),
                Name = gitRepository.ProjectReference.Name,
                Description = gitRepository.ProjectReference.Description,
                Url = new Uri(gitRepository.ProjectReference.Url.Replace("_apis/", string.Empty)),
                Platform = OwnerPlatform.AzureDevOps,
            }
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
        var status = gitPullRequest.Status switch
        {
            PullRequestStatus.NotSet => Shared.Models.PullRequestStatus.Draft,
            PullRequestStatus.Active => Shared.Models.PullRequestStatus.Active,
            PullRequestStatus.Abandoned => Shared.Models.PullRequestStatus.Abandoned,
            PullRequestStatus.Completed => Shared.Models.PullRequestStatus.Completed,
            PullRequestStatus.All => Shared.Models.PullRequestStatus.Unknown,
            _ => Shared.Models.PullRequestStatus.Unknown
        };

        return new AzureDevOpsPullRequest
        {
            Id = gitPullRequest.PullRequestId.ToString(),
            Name = gitPullRequest.Title,
            Description = gitPullRequest.Description,
            Url = new Uri(gitPullRequest.Url),
            Labels = gitPullRequest.Labels?.Select(l => l.Name).ToImmutableHashSet() ?? [],
            Reviewers = gitPullRequest.Reviewers?.Select(r => r.DisplayName).ToImmutableHashSet() ?? [],
            Status = status,
            Platform = PullRequestPlatform.AzureDevOps,
            LastCommit = null,
            RepositoryName = gitPullRequest.Repository?.Name ?? string.Empty,
            RepositoryUrl = new Uri(gitPullRequest.Repository?.Url ?? string.Empty),
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
            Relations = workItem.Relations?.Select(r => r.Title).ToImmutableHashSet() ?? []
        };
    }
}