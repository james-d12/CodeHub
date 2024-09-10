using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace CodeHub.Engine.AzureDevOps.Models;

public sealed record AzureDevOpsRepository
{
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required string DefaultBranch { get; init; }
    public required string Project { get; init; }
    public required string ProjectUrl { get; init; }
    public required bool IsDisabled { get; init; }
    public required bool IsInMaintenance { get; init; }

    public static AzureDevOpsRepository MapFromGitRepository(GitRepository gitRepository)
    {
        return new AzureDevOpsRepository
        {
            Name = gitRepository.Name,
            Url = gitRepository.WebUrl,
            DefaultBranch = gitRepository.DefaultBranch,
            Project = gitRepository.ProjectReference.Name,
            ProjectUrl = gitRepository.ProjectReference.Url,
            IsDisabled = gitRepository.IsDisabled ?? false,
            IsInMaintenance = gitRepository.IsInMaintenance ?? false
        };
    }
}