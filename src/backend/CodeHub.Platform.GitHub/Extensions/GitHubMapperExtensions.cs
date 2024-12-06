using CodeHub.Shared.Models;

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
            Project = new Project
            {
                Id = repository.Owner.Id.ToString(),
                Name = repository.Owner.Name,
                Description = repository.Owner.Bio,
                Url = new Uri(repository.Owner.Url),
                Platform = ProjectPlatform.GitHub,
            },
            Platform = RepositoryPlatform.GitHub
        };
    }
}