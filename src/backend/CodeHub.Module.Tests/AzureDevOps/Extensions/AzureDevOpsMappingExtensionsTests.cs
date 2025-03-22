using System.Collections.Immutable;
using AutoFixture;
using CodeHub.Domain.Git;
using CodeHub.Domain.Ticketing;
using CodeHub.Module.AzureDevOps.Extensions;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Identity;
using Microsoft.VisualStudio.Services.WebApi;
using BuildDefinitionReference = Microsoft.TeamFoundation.Build.WebApi.BuildDefinitionReference;
using WorkItem = Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItem;

namespace CodeHub.Module.Tests.AzureDevOps.Extensions;

public sealed class AzureDevOpsMappingExtensionsTests
{
    private readonly Fixture _fixture;

    public AzureDevOpsMappingExtensionsTests()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }

    [Fact]
    public void MapToAzureDevOpsPipeline_WhenGivenValidBuildDefinitionReference_ReturnsAzureDevOpsPipeline()
    {
        // Arrange
        var project = _fixture
            .Build<TeamProjectReference>()
            .With(t => t.Url, _fixture.Create<Uri>().ToString)
            .Create();
        var from = _fixture
            .Build<BuildDefinitionReference>()
            .With(b => b.Url, _fixture.Create<Uri>().ToString)
            .With(b => b.Project, project)
            .With(b => b.AuthoredBy, new IdentityRef())
            .Create();

        // Act
        var to = from.MapToAzureDevOpsPipeline();

        // Assert
        Assert.Equal(from.Id.ToString(), to.Id.Value);
        Assert.Equal(from.Name, to.Name);
        Assert.Equal(from.Url, to.Url.ToString());
        Assert.Equal(from.Path, to.Path);
        Assert.Equal(PipelinePlatform.AzureDevOps, to.Platform);
        Assert.Equal(from.Project.Id.ToString(), to.Owner.Id.Value);
        Assert.Equal(from.Project.Name, to.Owner.Name);
        Assert.Equal(from.Project.Description, to.Owner.Description);
        Assert.Equal(from.Project.Url, to.Owner.Url.ToString());
        Assert.Equal(OwnerPlatform.AzureDevOps, to.Owner.Platform);
    }

    [Fact]
    public void MapToAzureDevOpsProject_WhenGivenValidTeamProjectReference_ReturnsAzureDevOpsProject()
    {
        // Arrange
        var from = _fixture
            .Build<TeamProjectReference>()
            .With(t => t.Url, _fixture.Create<Uri>().ToString())
            .Create();

        // Act
        var to = from.MapToAzureDevOpsProject();

        // Assert
        Assert.Equal(from.Id, to.Id);
        Assert.Equal(from.Name, to.Name);
        Assert.Equal(from.Description, to.Description);
        Assert.Equal(from.Url, to.Url.ToString());
    }

    [Fact]
    public void MapToAzureDevOpsRepository_WhenGivenValidGitRepository_ReturnsAzureDevOpsRepository()
    {
        // Arrange
        var project = _fixture
            .Build<TeamProjectReference>()
            .With(t => t.Url, _fixture.Create<Uri>().ToString())
            .Create();

        var from = _fixture
            .Build<GitRepository>()
            .With(t => t.WebUrl, _fixture.Create<Uri>().ToString())
            .With(t => t.ProjectReference, project)
            .Create();

        // Act
        var to = from.MapToAzureDevOpsRepository();

        // Assert
        Assert.Equal(from.Id.ToString(), to.Id.Value);
        Assert.Equal(from.Name, to.Name);
        Assert.Equal(from.WebUrl, to.Url.ToString());
        Assert.Equal(RepositoryPlatform.AzureDevOps, to.Platform);
        Assert.Equal(from.IsDisabled, to.IsDisabled);
        Assert.Equal(from.IsInMaintenance, to.IsInMaintenance);
        Assert.Equal(from.DefaultBranch, to.DefaultBranch);
        Assert.Equal(from.ProjectReference.Id.ToString(), to.Owner.Id.Value);
        Assert.Equal(from.ProjectReference.Name, to.Owner.Name);
        Assert.Equal(from.ProjectReference.Url, to.Owner.Url.ToString());
        Assert.Equal(OwnerPlatform.AzureDevOps, to.Owner.Platform);
    }

    [Fact]
    public void MapToAzureDevOpsTeam_WhenGivenValidWebApiTeam_ReturnsAzureDevOpsTeam()
    {
        // Arrange
        var from = _fixture
            .Build<WebApiTeam>()
            .With(t => t.Url, _fixture.Create<Uri>().ToString())
            .With(t => t.Identity, new Identity())
            .Create();

        // Act
        var to = from.MapToAzureDevOpsTeam();

        // Assert
        Assert.Equal(from.Id, to.Id);
        Assert.Equal(from.Name, to.Name);
        Assert.Equal(from.Description, to.Description);
        Assert.Equal(from.Url, to.Url);
    }

    [Fact]
    public void MapToAzureDevOpsPullRequest_WhenGivenValidGitPullRequest_ReturnsAzureDevOpsPullRequest()
    {
        // Arrange
        var lastMergeCommit = _fixture
            .Build<GitCommitRef>()
            .With(g => g.Url, _fixture.Create<Uri>().ToString())
            .Without(g => g.Statuses)
            .Without(g => g.Push)
            .Create();

        var repository = _fixture
            .Build<GitRepository>()
            .With(g => g.Url, _fixture.Create<Uri>().ToString())
            .Create();

        var from = _fixture
            .Build<GitPullRequest>()
            .With(t => t.Repository, repository)
            .With(t => t.Url, _fixture.Create<Uri>().ToString())
            .With(t => t.ClosedBy, new IdentityRef())
            .With(t => t.CreatedBy, new IdentityRef())
            .With(t => t.AutoCompleteSetBy, new IdentityRef())
            .With(t => t.LastMergeCommit, lastMergeCommit)
            .With(t => t.LastMergeSourceCommit, new GitCommitRef())
            .With(t => t.LastMergeTargetCommit, new GitCommitRef())
            .Without(t => t.Reviewers)
            .Without(t => t.ForkSource)
            .Without(t => t.Commits)
            .Create();

        // Act
        var to = from.MapToAzureDevOpsPullRequest();

        // Assert
        Assert.Equal(from.PullRequestId.ToString(), to.Id.Value);
        Assert.Equal(from.Title, to.Name);
        Assert.Equal(from.Description, to.Description);
        Assert.Equal(from.Url, to.Url.ToString());
        Assert.NotNull(from.Labels);
        Assert.Equal(from.Labels.Select(l => l.Name).ToImmutableHashSet(), to.Labels);
    }

    [Fact]
    public void MapToAzureDevOpsWorkItem_WhenGivenValidWorkItem_ReturnsAzureDevOpsWorkItem()
    {
        // Arrange
        var fields = new Dictionary<string, object>()
        {
            { "System.Id", "" },
            { "System.Title", "TestThing" },
            { "System.State", "To Do" },
            { "System.WorkItemType", "User Story" },
            { "System.Description", "TestThing" }
        };

        var from = _fixture
            .Build<WorkItem>()
            .With(w => w.Fields, fields)
            .Create();

        // Act
        var to = from.MapToAzureDevOpsWorkItem();

        // Assert
        Assert.Equal(from.Id.ToString(), to.Id.Value);
        Assert.Equal(from.Url, to.Url);
        Assert.Equal(from.Fields["System.Title"], to.Title);
        Assert.Equal(string.Empty, to.Description);
        Assert.Equal(from.Fields["System.State"], to.State);
        Assert.Equal(from.Fields["System.WorkItemType"], to.Type);
        Assert.Equal(from.Fields, to.Fields);
        Assert.Equal(from.Relations.Select(r => r.Title).ToImmutableHashSet(), to.Relations);
        Assert.Equal(from.Rev, to.Revision);
        Assert.Equal(WorkItemPlatform.AzureDevOps, to.Platform);
    }
}