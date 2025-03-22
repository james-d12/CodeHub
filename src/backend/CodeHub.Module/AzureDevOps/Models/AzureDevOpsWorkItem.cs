using System.Collections.Frozen;
using System.Collections.Immutable;
using CodeHub.Domain.Ticketing;

namespace CodeHub.Module.AzureDevOps.Models;

public sealed record AzureDevOpsWorkItem : WorkItem
{
    public required string Url { get; init; }
    public required int Revision { get; init; }
    public required FrozenDictionary<string, object> Fields { get; init; }
    public required ImmutableHashSet<string> Relations { get; init; }
}