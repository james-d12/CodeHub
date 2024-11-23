using System.Text.Json.Serialization;

namespace CodeHub.Platform.Soos.Models;

[JsonSerializable(typeof(SoosProjectSetting))]
public sealed record SoosProjectSetting
{
    [JsonPropertyName("dependencyDepth")]
    public required int DependencyDepth { get; init; }

    [JsonPropertyName("includeDevDependencies")]
    public required bool IncludeDevDependencies { get; init; }

    [JsonPropertyName("containerDependencySource")]
    public required string ContainerDependencySource { get; init; }

    [JsonPropertyName("sbomDependencySource")]
    public required string SbomDependencySource { get; init; }

    [JsonPropertyName("enableGitHubWebhook")]
    public required object EnableGitHubWebhook { get; init; }

    [JsonPropertyName("buildVersionFile")]
    public required object BuildVersionFile { get; init; }

    [JsonPropertyName("failBuildThresholds")]
    public required FailBuildThreshold[] FailBuildThresholds { get; init; }

    [JsonPropertyName("internalPackages")]
    public required string[] InternalPackages { get; init; }

    [JsonPropertyName("useLockFile")]
    public required object UseLockFile { get; init; }

    [JsonPropertyName("branchSettings")]
    public required BranchSetting[] BranchSettings { get; init; }

    [JsonPropertyName("gitHubPullRequestSettings")]
    public required GitHubPullRequestSetting GitHubPullRequestSetting { get; init; }

    [JsonPropertyName("sastImportThreshold")]
    public required string SastImportThreshold { get; init; }

    [JsonPropertyName("vulnerabilityComplianceThresholds")]
    public required VulnerabilityComplianceThreshold[] VulnerabilityComplianceThresholds { get; init; }

    [JsonPropertyName("issueManagementSettings")]
    public required IssueManagementSetting IssueManagementSetting { get; init; }

    [JsonPropertyName("enableNotifications")]
    public required bool EnableNotifications { get; init; }

    [JsonPropertyName("notificationEmailAddress")]
    public required string NotificationEmailAddress { get; init; }

    [JsonPropertyName("notificationRules")]
    public required NotificationRule[] NotificationRules { get; init; }
}

[JsonSerializable(typeof(FailBuildThreshold))]
public sealed record FailBuildThreshold
{
    [JsonPropertyName("source")]
    public required string Source { get; init; }

    [JsonPropertyName("severity")]
    public required string Severity { get; init; }
}

[JsonSerializable(typeof(BranchSetting))]
public sealed record BranchSetting
{
    [JsonPropertyName("branchFilter")]
    public required string BranchFilter { get; init; }

    [JsonPropertyName("performRescan")]
    public required bool PerformRescan { get; init; }

    [JsonPropertyName("retentionPeriodDays")]
    public required int? RetentionPeriodDays { get; init; }
}

[JsonSerializable(typeof(GitHubPullRequestSetting))]
public sealed record GitHubPullRequestSetting
{
    [JsonPropertyName("pinVersion")]
    public required object PinVersion { get; init; }

    [JsonPropertyName("targetBranch")]
    public required string TargetBranch { get; init; }

    [JsonPropertyName("labels")]
    public required object[] Labels { get; init; }

    [JsonPropertyName("assignees")]
    public required object[] Assignees { get; init; }

    [JsonPropertyName("branchNameSeparator")]
    public required string BranchNameSeparator { get; init; }

    [JsonPropertyName("prefix")]
    public required object Prefix { get; init; }
}

[JsonSerializable(typeof(VulnerabilityComplianceThreshold))]
public sealed record VulnerabilityComplianceThreshold
{
    [JsonPropertyName("severity")]
    public required string Severity { get; init; }

    [JsonPropertyName("days")]
    public required int Days { get; init; }

    [JsonPropertyName("breakWhenOutOfCompliance")]
    public required bool BreakWhenOutOfCompliance { get; init; }
}

[JsonSerializable(typeof(IssueManagementSetting))]
public sealed record IssueManagementSetting
{
    [JsonPropertyName("defaultFixAction")]
    public required string DefaultFixAction { get; init; }

    [JsonPropertyName("fixProjectId")]
    public required string FixProjectId { get; init; }

    [JsonPropertyName("closeIssuesWhenResolved")]
    public required bool CloseIssuesWhenResolved { get; init; }

    [JsonPropertyName("closeIssuesWhenAttested")]
    public required bool CloseIssuesWhenAttested { get; init; }

    [JsonPropertyName("autoCreateIssues")]
    public required object[] AutoCreateIssues { get; init; }
}

[JsonSerializable(typeof(NotificationRule))]
public sealed record NotificationRule
{
    [JsonPropertyName("issueSource")]
    public required string IssueSource { get; init; }

    [JsonPropertyName("severity")]
    public required string Severity { get; init; }

    [JsonPropertyName("frequency")]
    public required string Frequency { get; init; }
}