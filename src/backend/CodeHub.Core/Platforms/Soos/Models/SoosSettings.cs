namespace CodeHub.Core.Platforms.Soos.Models;

internal sealed record SoosSettings
{
    public required string Key { get; set; }
    public required string ClientId { get; set; }
}