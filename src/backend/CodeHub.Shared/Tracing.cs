using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CodeHub.Shared;

public readonly struct Tracing
{
    private static readonly ActivitySource ActivitySource = new(AppDomain.CurrentDomain.FriendlyName);

    public static Activity? StartActivity(
        [CallerMemberName]
        string? caller = null,
        [CallerFilePath]
        string? filePath = null)
    {
        var className =
            Path.GetFileNameWithoutExtension(filePath?.Split(Path.DirectorySeparatorChar).LastOrDefault() ??
                                             string.Empty);
        var fullMethodName = $"{className}.{caller}";

        return ActivitySource.StartActivity(fullMethodName);
    }
}