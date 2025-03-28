/// <summary>
/// Defines a strategy for writing data to a specified destination.
/// </summary>
public interface IWriteStrategy
{
  /// <summary>
  /// Gets the destination path where data will be written.
  /// </summary>
  string DestinationPath { get; }

  /// <summary>
  /// Sets up the write strategy with the specified destination path.
  /// </summary>
  /// <param name="destinationPath">The path to the destination where data will be written.</param>
  void Setup(string destinationPath);

  /// <summary>
  /// Writes the specified entries to the destination path.
  /// </summary>
  /// <param name="entries">The entries to write.</param>
  /// <param name="sip">The SIP object containing metadata.</param>
  /// <param name="fileNameWithoutExtension">The optional file name without extension.</param>
  /// <param name="fallbackName">The fallback name to use if no file name is provided.</param>
  /// <param name="deleteExisting">Indicates whether to delete existing files at the destination.</param>
  /// <returns>The path to the written file.</returns>
  string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string? fileNameWithoutExtension, string fallbackName, bool deleteExisting);

  /// <summary>
  /// Writes the specified entries to the destination path, optionally creating a SIP ID folder.
  /// </summary>
  /// <param name="entries">The entries to write.</param>
  /// <param name="sip">The SIP object containing metadata.</param>
  /// <param name="fileNameWithoutExtension">The optional file name without extension.</param>
  /// <param name="fallbackName">The fallback name to use if no file name is provided.</param>
  /// <param name="deleteExisting">Indicates whether to delete existing files at the destination.</param>
  /// <param name="createSipIdFolder">Indicates whether to create a folder for the SIP ID.</param>
  /// <returns>The path to the written file.</returns>
  string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string? fileNameWithoutExtension, string fallbackName, bool deleteExisting, bool createSipIdFolder);
}