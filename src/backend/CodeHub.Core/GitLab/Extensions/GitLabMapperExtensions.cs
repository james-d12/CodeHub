using System.Collections.Immutable;
using CodeHub.Core.GitLab.Models;
using CodeHub.Core.Shared.Models;
using NGitLab.Models;
using Commit = CodeHub.Core.Shared.Models.Commit;

namespace CodeHub.Core.GitLab.Extensions;

internal static class GitLabMapperExtensions
{
    internal static GitLabPullRequest MapToGitLabPullRequest(this MergeRequest mergeRequest)
    {
        return new GitLabPullRequest
        {
            Id = new PullRequestId(mergeRequest.Id.ToString()),
            Name = mergeRequest.Title,
            Description = mergeRequest.Description,
            Url = new Uri(mergeRequest.WebUrl),
            Labels = mergeRequest.Labels.ToImmutableHashSet(),
            Reviewers = mergeRequest.Reviewers.Select(r => r.Name).ToImmutableHashSet(),
            Status = PullRequestStatus.Draft,
            Platform = PullRequestPlatform.GitLab,
            LastCommit = new Commit
            {
                Id = new CommitId(""),
                Url = new Uri(mergeRequest.WebUrl),
                Committer = string.Empty,
                Comment = string.Empty,
                ChangeCount = 0
            },
            RepositoryUrl = new Uri(mergeRequest.WebUrl),
            RepositoryName = string.Empty,
            CreatedOnDate = DateOnly.FromDateTime(mergeRequest.CreatedAt)
        };
    }

    internal static GitLabPipeline MapToGitLabPipeline(this PipelineBasic pipeline)
    {
        return new GitLabPipeline
        {
            Id = new PipelineId(pipeline.Id.ToString()),
            Name = pipeline.Name,
            Url = new Uri(pipeline.WebUrl),
            Owner = Owner.CreateEmptyOwner(),
            Platform = PipelinePlatform.GitLab
        };
    }

    internal static GitLabRepository MapToGitLabRepository(this Project project)
    {
        return new GitLabRepository
        {
            Id = new RepositoryId(project.Id.ToString()),
            Name = project.Name,
            Url = new Uri(project.WebUrl),
            DefaultBranch = project.DefaultBranch,
            Owner = new Owner
            {
                Id = new OwnerId(project.Owner.Id.ToString()),
                Name = project.Owner.Name,
                Description = project.Owner.Bio,
                Url = new Uri(project.Owner.WebURL),
                Platform = OwnerPlatform.AzureDevOps
            },
            Platform = RepositoryPlatform.GitLab
        };
    }
}