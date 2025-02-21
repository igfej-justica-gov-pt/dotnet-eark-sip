using System.Reflection;

public class VersionProvider {
  public static string? GetVersion() {
    string? implementationVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    return implementationVersion;
  }

  public static string GetVersionComplete() {
    return $"dotnet-eark-sip version {GetVersion() ?? "unknown"}";
  }
}