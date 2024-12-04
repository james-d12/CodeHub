namespace CodeHub.Shared.Models;

public enum SecurityAnalysisPlatform
{
    SooS,
    Snyk
}

public record SecurityAnalysis
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required SecurityAnalysisPlatform Platform { get; set; }
}