namespace CodeHub.Core.Models;

public enum SecurityAnalysisPlatform
{
    SooS,
    Snyk
}

public abstract record SecurityAnalysisResource
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required SecurityAnalysisPlatform Platform { get; set; }
}