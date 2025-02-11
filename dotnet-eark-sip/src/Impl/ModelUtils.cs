using System.Text;
using Microsoft.Extensions.Logging;

public static class ModelUtils {
  public static void DeleteBuildDir(string buildDir) {
    try {
      Utils.DeleteDirectory(buildDir);
    } catch (IOException e) {
      throw new IPException("Error deleting temporary directory created to hold IP files: " + buildDir, e);
    }
  }

  public static DirectoryInfo CreateBuildDir(string path) {
    try {
      return Directory.CreateDirectory(path);
    } catch (IOException e) {
      throw new IPException("Unable to create directory to hold IP files, in path'" + path + "'", e);
    }
  }

  public static string GetFoldersFromList(List<string> folders) {
    StringBuilder sb = new StringBuilder();
    foreach (string folder in folders) {
      sb.Append(folder);
      if (sb.Length > 0) sb.Append(IPConstants.ZIP_PATH_SEPARATOR);
    }

    return sb.ToString();
  }

  public static void CleanUpUponInterrupt(ILogger logger, string path) {
    try {
      Utils.DeleteDirectory(path);
    } catch (IOException e) {
      logger.LogError(e, "Error cleaning up unneeded files");
    }
  }
}