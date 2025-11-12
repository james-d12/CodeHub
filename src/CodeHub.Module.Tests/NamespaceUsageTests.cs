using System.Text.RegularExpressions;

namespace CodeHub.Module.Tests;

public sealed class NamespaceUsageTests
{
    private static readonly string[] AllowedNamespaces =
    {
        "CodeHub.Module.Shared"
    };

    private static string GetRootPath()
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var solutionDir =
            Directory.GetParent(baseDir)?.Parent?.Parent?.Parent?.Parent?.FullName ?? string.Empty;
        return Path.Combine(solutionDir, "CodeHub.Module");
    }

    [Theory]
    [InlineData("Azure")]
    [InlineData("AzureDevOps")]
    [InlineData("GitHub")]
    [InlineData("GitLab")]
    public void UsingStatements_ShouldOnlyAllowThirdPartySelfAndSharedUsings(string moduleName)
    {
        var rootPath = GetRootPath();
        var modulePath = Path.Combine(rootPath, moduleName);
        var files = Directory.GetFiles(modulePath, "*.cs", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            var content = File.ReadAllText(file);
            var usings = Regex.Matches(content, @"^\s*using\s+([\w\.]+);", RegexOptions.Multiline)
                .Select(m => m.Groups[1].Value)
                .ToList();

            foreach (var usedNamespace in usings)
            {
                if (!usedNamespace.StartsWith("CodeHub.Module")) continue;

                var isAllowed = usedNamespace.StartsWith($"CodeHub.Module.{moduleName}.") ||
                                AllowedNamespaces.Any(n => usedNamespace.StartsWith(n));
                Assert.True(isAllowed, $"Disallowed namespace '{usedNamespace}' found in file '{file}'");
            }
        }
    }
}