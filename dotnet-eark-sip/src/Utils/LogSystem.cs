using System.Text;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides logging utilities and system information logging.
/// </summary>
public class LogSystem
{
    /// <summary>
    /// Provides a logger instance for the LogSystem class.
    /// </summary>
    public static readonly ILogger logger = DefaultLogger.Create<LogSystem>();
    /// <summary>
    /// Represents the default value used when a property is unknown.
    /// </summary>
    public static string UNKNOWN = "unknown";

    private LogSystem() { }

    private static Dictionary<string, string> GetOperatingSystemInfo()
    {
        Dictionary<string, string> result = new()
        {
            ["Operating System"] = GetProperty("OS", UNKNOWN),
            ["Architecture"] = GetProperty("PROCESSOR_ARCHITECTURE", UNKNOWN),
            ["Version"] = Environment.OSVersion.ToString(),
            [".NET Version"] = Environment.Version.ToString(),
            ["System Locale"] = System.Globalization.CultureInfo.CurrentCulture.Name,
            ["Default Charset used by StreamWriter"] = GetDefaultCharSet(),
            ["file.encoding property"] = Encoding.Default.EncodingName,
        };

        return result;
    }

    private static string GetProperty(string key, string defaultValue)
    {
        return Environment.GetEnvironmentVariable(key) ?? defaultValue;
    }

    private static string GetDefaultCharSet()
    {
        using (var dummy = new StreamWriter(new MemoryStream()))
        {
            return dummy.Encoding.WebName;
        }
    }

    /// <summary>
    /// Logs detailed information about the operating system and environment.
    /// </summary>
    public static void LogOperatingSystemInfo()
    {
        foreach (KeyValuePair<string, string> entry in GetOperatingSystemInfo())
        {
            logger.LogDebug("{0}: {1}", entry.Key, entry.Value);
        }
    }
}