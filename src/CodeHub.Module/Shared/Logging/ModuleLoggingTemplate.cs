using Microsoft.Extensions.Logging;

namespace CodeHub.Module.Shared.Logging;

public static partial class ModuleLoggingTemplate
{
    [LoggerMessage(1000, LogLevel.Information, "Querying {Resource} from {Platform}.")]
    public static partial void QueryingResource(this ILogger logger, string resource, string platform);
}