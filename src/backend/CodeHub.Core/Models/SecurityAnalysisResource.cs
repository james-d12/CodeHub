namespace CodeHub.Engine.Models;

public enum SecurityAnalysisPlatform
{
    SooS,
    Snyk
}

public abstract class SecurityAnalysisResource
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required SecurityAnalysisPlatform Platform { get; set; }
}