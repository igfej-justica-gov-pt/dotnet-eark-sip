using System.Text;
using Microsoft.Extensions.Logging;

public class LogSystem {
  public static readonly ILogger logger = DefaultLogger.Create<LogSystem>();
  public static string UNKNOWN = "unknown";

  private LogSystem() {}

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

  private static string GetProperty(string key, string defaultValue) {
    return Environment.GetEnvironmentVariable(key) ?? defaultValue;
  }

  private static string GetDefaultCharSet() {
    using (var dummy = new StreamWriter(new MemoryStream())) {
      return dummy.Encoding.WebName;
    }
  }

  public static void LogOperatingSystemInfo() {
    foreach (KeyValuePair<string, string> entry in GetOperatingSystemInfo()) {
      logger.LogDebug("{0}: {1}", entry.Key, entry.Value);
    }
  }
}