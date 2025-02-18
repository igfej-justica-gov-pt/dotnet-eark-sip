using System.Reflection;

public class VersionProvider {
  public static string GetVersion() {
    string? implementationVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    return $"dotnet-eark-sip version {implementationVersion}";
  }
}