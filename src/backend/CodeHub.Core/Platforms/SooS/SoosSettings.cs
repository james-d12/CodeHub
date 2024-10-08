namespace CodeHub.Core.Platforms.SooS;

public sealed record SoosSettings
{
    public required string Key { get; set; }
    public required string ClientId { get; set; }
}