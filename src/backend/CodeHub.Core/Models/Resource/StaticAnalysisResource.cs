namespace CodeHub.Core.Models.Resource;

public enum StaticAnalysisPlatform
{
    SonarCloud
}

public abstract record StaticAnalysisResource
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required StaticAnalysisPlatform Platform { get; set; }
}