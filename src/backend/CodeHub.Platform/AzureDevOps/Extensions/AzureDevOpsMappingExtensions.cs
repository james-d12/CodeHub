using CodeHub.Core.Models;
using CodeHub.Platform.AzureDevOps.Models;
using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

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
            Name = webApiTeam.Name,
            Description = webApiTeam.Description,
            Url = webApiTeam.Url
        };
    }
}