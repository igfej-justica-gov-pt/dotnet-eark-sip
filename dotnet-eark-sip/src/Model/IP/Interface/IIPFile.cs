namespace IP
{
  /// <summary>
  /// Represents an interface for IP file operations.
  /// </summary>
  public interface IIPFile
  {
    /// <summary>
    /// Gets a list of relative folder paths.
    /// </summary>
    /// <returns>A list of relative folder paths.</returns>
    List<string> GetRelativeFolders();

    /// <summary>
    /// Gets the file name.
    /// </summary>
    /// <returns>The file name, or null if not available.</returns>
    string? GetFileName();

    /// <summary>
    /// Gets the full file path.
    /// </summary>
    /// <returns>The full file path.</returns>
    string GetPath();
  }
}