using System.Collections.Frozen;
using System.Collections.Immutable;

namespace CodeHub.Platform.AzureDevOps.Models;

public sealed record AzureDevOpsWorkItem
{
    public required int Id { get; init; }
    public required string Url { get; init; }
    public required int Revision { get; init; }
    public required FrozenDictionary<string, object> Fields { get; init; }
    public required ImmutableHashSet<string> Relations { get; init; }
}