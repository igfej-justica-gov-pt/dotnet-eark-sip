using System.Text;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides utility methods for handling models, such as directory operations and folder manipulations.
/// </summary>
public static class ModelUtils {
  /// <summary>
  /// Deletes the specified build directory.
  /// </summary>
  /// <param name="buildDir">The path to the build directory to delete.</param>
  public static void DeleteBuildDir(string buildDir) {
    try {
      Utils.DeleteDirectory(buildDir);
    } catch (IOException e) {
      throw new IPException("Error deleting temporary directory created to hold IP files: " + buildDir, e);
    }
  }

  /// <summary>
  /// Creates a directory at the specified path.
  /// </summary>
  /// <param name="path">The path where the directory will be created.</param>
  /// <returns>A DirectoryInfo object representing the created directory.</returns>
  public static DirectoryInfo CreateBuildDir(string path) {
    try {
      return Directory.CreateDirectory(path);
    } catch (IOException e) {
      throw new IPException("Unable to create directory to hold IP files, in path'" + path + "'", e);
    }
  }

  /// <summary>
  /// Combines a list of folder names into a single string separated by a defined path separator.
  /// </summary>
  /// <param name="folders">The list of folder names to combine.</param>
  /// <returns>A string containing the combined folder names separated by the path separator.</returns>
  public static string GetFoldersFromList(List<string> folders) {
    StringBuilder sb = new StringBuilder();
    foreach (string folder in folders) {
      sb.Append(folder);
      if (sb.Length > 0) sb.Append(IPConstants.ZIP_PATH_SEPARATOR);
    }

    return sb.ToString();
  }

  /// <summary>
  /// Cleans up files at the specified path upon an interrupt.
  /// </summary>
  /// <param name="logger">The logger to log any errors during cleanup.</param>
  /// <param name="path">The path to the directory to clean up.</param>
  public static void CleanUpUponInterrupt(ILogger logger, string path) {
    try {
      Utils.DeleteDirectory(path);
    } catch (IOException e) {
      logger.LogError(e, "Error cleaning up unneeded files");
    }
  }
}