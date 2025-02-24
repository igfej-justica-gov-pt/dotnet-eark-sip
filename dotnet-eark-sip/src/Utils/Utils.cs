using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Runtime.InteropServices;

public static class Utils {
  private static readonly ILogger logger = DefaultLogger.Create("Utils");

  public static bool IsWindowsSystem() {
    return RuntimeInformation.OSDescription.ToLower().Contains("windows");
  }

  public static string GenerateRandomAndPrefixedUUID() {
    return METSEnums.ID_PREFIX + Guid.NewGuid().ToString().ToUpper();
  }

  public static string GenerateRandomAndPrefixedFileID() {
    return METSEnums.FILE_ID_PREFIX + Guid.NewGuid().ToString().ToUpper();
  }

  public static string CopyResourceFromClasspathToDir(Type resourceType, string dir, string resourceTempSuffix, string resourcePath) {
    string resource = Path.Combine(dir, Path.GetRandomFileName() + resourceTempSuffix);

    Assembly assembly = resourceType.Assembly;
    using (Stream inputStream = assembly.GetManifestResourceStream(resourcePath)) {
      if (inputStream == null) throw new FileNotFoundException($"Embedded resource not found: {resourcePath}");

      using (FileStream outputStream = File.Create(resource)) {
        inputStream.CopyTo(outputStream);
      }
    }

    return resource;
  }

  public static void DeleteDirectory(string path) {
    if (path == null) return;

    string[] files = Directory.GetFiles(path) ?? new string[0];
    if (files.Length > 0) {
      logger.LogDebug("Directory {path} is not empty. Going to delete its content as well.", path);
      foreach (string file in files) {
        File.Delete(file);
      }
    }

    Directory.Delete(path);
  }
}