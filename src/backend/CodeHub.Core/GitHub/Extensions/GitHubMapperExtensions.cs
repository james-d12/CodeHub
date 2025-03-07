using CodeHub.Core.Shared.Models;
using Octokit;
using Repository = CodeHub.Core.Shared.Models.Repository;

namespace CodeHub.Core.GitHub.Extensions;

public static class GitHubMapperExtensions
{
    public static Repository MapToRepository(this Octokit.Repository repository)
    {
        return new Repository
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
                Url = new Uri(repository.Owner.Url),
                Platform = OwnerPlatform.GitHub,
            },
            Platform = RepositoryPlatform.GitHub
        };
    }

    public static Pipeline MapToPipeline(this Workflow workflow)
    {
        return new Pipeline
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

    public static Pipeline MapToPipeline((Workflow workflow, WorkflowRun run) pipeline)
    {
        return new Pipeline
        {
            Id = new PipelineId(pipeline.workflow.Id.ToString()),
            Name = pipeline.workflow.Name,
            Url = new Uri(pipeline.workflow.HtmlUrl),
            Owner = new Owner
            {
                Id = new OwnerId(string.Empty),
                Name = string.Empty,
                Description = string.Empty,
                Url = new Uri(pipeline.workflow.HtmlUrl),
                Platform = OwnerPlatform.GitHub
            },
            Platform = PipelinePlatform.GitHub
        };
    }
}