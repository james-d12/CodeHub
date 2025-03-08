using System.Collections.Immutable;
using CodeHub.Domain.Git;
using CodeHub.Module.GitHub.Models;
using Octokit;

namespace CodeHub.Module.GitHub.Extensions;

public static class GitHubMapperExtensions
{
    public static GitHubRepository MapToGitHubRepository(this Octokit.Repository repository)
    {
        return new GitHubRepository
        {
            Id = new RepositoryId(repository.Id.ToString()),
            Name = repository.Name,
            Url = new Uri(repository.HtmlUrl),
            DefaultBranch = repository.DefaultBranch,
            Owner = new Owner
            {
                Id = new OwnerId(repository.Owner.Id.ToString()),
                Name = repository.Owner.Login,
                Description = repository.Owner.Bio,
                Url = new Uri(repository.Owner.HtmlUrl),
                Platform = OwnerPlatform.GitHub,
            },
            Platform = RepositoryPlatform.GitHub
        };
    }

    public static GitHubPipeline MapToGitHubPipeline(this Workflow workflow)
    {
        return new GitHubPipeline
        {
            Id = new PipelineId(workflow.Id.ToString()),
            Name = workflow.Name,
            Url = new Uri(workflow.HtmlUrl),
            Owner = new Owner
            {
                Id = new OwnerId(string.Empty),
                Name = string.Empty,
                Description = string.Empty,
                Url = new Uri(workflow.HtmlUrl),
                Platform = OwnerPlatform.GitHub
            },
            Platform = PipelinePlatform.GitHub
        };
    }

    public static GitHubPullRequest MapToGitHubPullRequest(this Octokit.PullRequest pullRequest,
        GitHubRepository repository)
    {
        var status = pullRequest.State.Value switch
        {
            ItemState.Open => PullRequestStatus.Active,
            ItemState.Closed => PullRequestStatus.Completed,
            _ => PullRequestStatus.Unknown
        };

        return new GitHubPullRequest
        {
            Id = new PullRequestId(pullRequest.Id.ToString()),
            Name = pullRequest.Title,
            Description = pullRequest.Title,
            Url = new Uri(pullRequest.HtmlUrl),
            Labels = pullRequest.Labels.Select(l => l.Name).ToImmutableHashSet(),
            Reviewers = pullRequest.RequestedReviewers.Select(r => r.Name).ToImmutableHashSet(),
            Status = status,
            Platform = PullRequestPlatform.GitHub,
            LastCommit = null,
            RepositoryUrl = repository.Url,
            RepositoryName = repository.Name,
            CreatedOnDate = DateOnly.FromDateTime(pullRequest.CreatedAt.UtcDateTime)
        };
    }
}