﻿using CodeHub.Shared.Models;

namespace CodeHub.Platform.GitHub.Extensions;

internal static class GitHubMapperExtensions
{
    internal static Repository MapToRepository(this Octokit.Repository repository)
    {
        return new Repository
        {
            Id = repository.Id.ToString(),
            Name = repository.Name,
            Url = new Uri(repository.HtmlUrl),
            DefaultBranch = repository.DefaultBranch,
            Owner = new Owner
            {
                Id = repository.Owner.Id.ToString(),
                Name = repository.Owner.Login,
                Description = repository.Owner.Bio,
                Url = new Uri(repository.Owner.Url),
                Platform = OwnerPlatform.GitHub,
            },
            Platform = RepositoryPlatform.GitHub
        };
    }

    internal static Pipeline MapToPipeline(this Octokit.Workflow workflow)
    {
        return new Pipeline
        {
            Id = workflow.Id.ToString(),
            Name = workflow.Name,
            Url = new Uri(workflow.HtmlUrl),
            Owner = new Owner
            {
                Id = string.Empty,
                Name = string.Empty,
                Description = string.Empty,
                Url = new Uri(workflow.HtmlUrl),
                Platform = OwnerPlatform.GitHub
            },
            Platform = PipelinePlatform.GitHub
        };
    }
}