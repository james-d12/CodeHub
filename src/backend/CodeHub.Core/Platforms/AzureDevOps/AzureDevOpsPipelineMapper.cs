﻿using CodeHub.Core.Models;
using Microsoft.TeamFoundation.Build.WebApi;

namespace CodeHub.Core.Platforms.AzureDevOps;

internal static class AzureDevOpsPipelineMapper
{
    internal static AzureDevOpsPipeline MapFromBuildDefinitionReference(
        BuildDefinitionReference buildDefinitionReference)
    {
        return new AzureDevOpsPipeline
        {
            Id = buildDefinitionReference.Id.ToString(),
            Name = buildDefinitionReference.Name,
            Url = buildDefinitionReference.Url,
            Project = buildDefinitionReference.Project.Name,
            ProjectName = buildDefinitionReference.Project.Url,
            Path = buildDefinitionReference.Path,
            Platform = PipelinePlatform.AzureDevOps
        };
    }
}