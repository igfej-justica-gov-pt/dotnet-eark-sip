/// <summary>
/// Implements a strategy for writing data to a ZIP file.
/// </summary>
public class ZipWriteStrategy : IWriteStrategy
{
  /// <summary>
  /// Gets the destination path where data will be written.
  /// </summary>
  public string DestinationPath { get; private set; } = "";

  /// <summary>
  /// Sets up the write strategy with the specified destination path.
  /// </summary>
  /// <param name="destinationPath">The path to the destination where data will be written.</param>
  public void Setup(string destinationPath)
  {
    DestinationPath = destinationPath;
    if (!Directory.Exists(destinationPath)) Directory.CreateDirectory(destinationPath);
  }

  /// <summary>
  /// Writes the specified entries to the destination path.
  /// </summary>
  /// <param name="entries">The entries to write.</param>
  /// <param name="sip">The SIP object containing metadata.</param>
  /// <param name="fileNameWithoutExtension">The optional file name without extension.</param>
  /// <param name="fallbackName">The fallback name to use if no file name is provided.</param>
  /// <param name="deleteExisting">Indicates whether to delete existing files at the destination.</param>
  /// <returns>The path to the written file.</returns>
  public string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string? fileNameWithoutExtension, string fallbackName, bool deleteExisting)
  {
    return Write(entries, sip, fileNameWithoutExtension, fallbackName, true, deleteExisting);
  }

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
  public string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string? fileNameWithoutExtension, string fallbackName, bool createSipIdFolder, bool deleteExisting)
  {
    string zipPath = GetZipPath(DestinationPath, fileNameWithoutExtension, fallbackName);

    try
    {
      using (FileStream fileStream = new(zipPath, FileMode.Create))
      {
        ZIPUtils.Zip(entries, fileStream, sip, createSipIdFolder, true);
      }
    }
    catch (OperationCanceledException e)
    {
      throw new ThreadInterruptedException("Operation canceled.", e);
    }
    catch (IOException e)
    {
      throw new IPException("Error generating E-ARK SIP ZIP file. Reason: " + e.Message, e);
    }

    return zipPath;
  }

  private string GetZipPath(string destinationDirectory, string? fileNameWithoutExtension, string fallbackName)
  {
    string zipPath;

    if (fileNameWithoutExtension != null)
    {
      zipPath = Path.Combine(destinationDirectory, fileNameWithoutExtension + IPConstants.SIP_FILE_EXTENSION);
    }
    else
    {
      zipPath = Path.Combine(destinationDirectory, fallbackName + IPConstants.SIP_FILE_EXTENSION);
    }

    try
    {
      if (File.Exists(zipPath)) File.Delete(zipPath);
    }
    catch (IOException e)
    {
      throw new IPException("Error deleting already existing zip", e);
    }

    return zipPath;
  }
}