using System.Collections.Immutable;
using CodeHub.Platform.GitLab.Models;
using CodeHub.Shared.Models;
using NGitLab.Models;
using Commit = CodeHub.Shared.Models.Commit;

namespace CodeHub.Platform.GitLab.Extensions;

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
}