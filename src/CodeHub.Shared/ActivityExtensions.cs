using System.Diagnostics;

namespace CodeHub.Shared;

public static class ActivityExtensions
{
    public static void RecordException(this Activity? activity, Exception exception)
    {
        activity?.AddException(exception);
        activity?.SetStatus(ActivityStatusCode.Error);
    }
}