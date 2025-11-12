using System.Collections.Immutable;
using AutoFixture;
using CodeHub.Domain.Git;
using CodeHub.Module.GitHub.Extensions;
using CodeHub.Module.GitHub.Models;
using Octokit;
using PullRequest = Octokit.PullRequest;

namespace CodeHub.Module.Tests.GitHub.Extensions;

public sealed class GitHubMapperExtensionsTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void MapToGitHubPullRequest_WhenGivenValidPullRequest_ReturnsGitHubPullRequest()
    {
        // Arrange
        var repository = _fixture.Create<GitHubRepository>();
        var from = _fixture
            .Build<PullRequest>()
            .FromFactory(() => new PullRequest(
                id: _fixture.Create<long>(),
                nodeId: _fixture.Create<string>(),
                url: _fixture.Create<Uri>().ToString(),
                htmlUrl: _fixture.Create<Uri>().ToString(),
                diffUrl: _fixture.Create<Uri>().ToString(),
                patchUrl: _fixture.Create<Uri>().ToString(),
                issueUrl: _fixture.Create<Uri>().ToString(),
                statusesUrl: _fixture.Create<Uri>().ToString(),
                number: _fixture.Create<int>(),
                state: ItemState.Open,
                title: _fixture.Create<string>(),
                body: _fixture.Create<string>(),
                createdAt: _fixture.Create<DateTimeOffset>(),
                updatedAt: _fixture.Create<DateTimeOffset>(),
                closedAt: _fixture.Create<DateTimeOffset>(),
                mergedAt: _fixture.Create<DateTimeOffset>(),
                head: _fixture.Create<GitReference>(),
                @base: _fixture.Create<GitReference>(),
                user: _fixture.Create<User>(),
                assignee: _fixture.Create<User>(),
                assignees: _fixture.Create<IReadOnlyList<User>>(),
                draft: _fixture.Create<bool>(),
                mergeable: _fixture.Create<bool>(),
                mergeableState: _fixture.Create<MergeableState>(),
                mergedBy: _fixture.Create<User>(),
                mergeCommitSha: _fixture.Create<string>(),
                comments: _fixture.Create<int>(),
                commits: _fixture.Create<int>(),
                additions: _fixture.Create<int>(),
                deletions: _fixture.Create<int>(),
                changedFiles: _fixture.Create<int>(),
                milestone: _fixture.Create<Milestone>(),
                locked: _fixture.Create<bool>(),
                maintainerCanModify: _fixture.Create<bool>(),
                requestedReviewers: _fixture.Create<IReadOnlyList<User>>(),
                requestedTeams: _fixture.Create<IReadOnlyList<Team>>(),
                labels: _fixture.Create<IReadOnlyList<Label>>(),
                activeLockReason: _fixture.Create<LockReason>()
            ))
            .Create();

        // Act
        var to = from.MapToGitHubPullRequest(repository);

        // Assert
        Assert.Equal(from.Id.ToString(), to.Id.Value);
        Assert.Equal(from.Title, to.Name);
        Assert.Equal(from.Title, to.Description);
        Assert.Equal(PullRequestPlatform.GitHub, to.Platform);
        Assert.Equal(from.Labels.Select(l => l.Name).ToImmutableHashSet(), to.Labels);
        Assert.Equal(from.RequestedReviewers.Select(r => r.Name).ToImmutableHashSet(), to.Reviewers);
        Assert.Equal(PullRequestStatus.Active, to.Status);
        Assert.Null(to.LastCommit);
        Assert.Equal(repository.Name, to.RepositoryName);
        Assert.Equal(repository.Url, to.RepositoryUrl);
        Assert.Equal(DateOnly.FromDateTime(from.CreatedAt.UtcDateTime), to.CreatedOnDate);
    }
}