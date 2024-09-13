namespace CodeHub.Engine.Models;

public enum StaticAnalysisPlatform
{
    SonarCloud
}

public abstract class StaticAnalysisResource
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required StaticAnalysisPlatform Platform { get; set; }
}