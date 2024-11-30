namespace CodeHub.Shared.Models;

public enum StaticAnalysisPlatform
{
    SonarCloud
}

public abstract record StaticAnalysis
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required StaticAnalysisPlatform Platform { get; set; }
}