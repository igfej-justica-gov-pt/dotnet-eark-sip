public class ZipWriteStrategy : IWriteStrategy {
  public string DestinationPath { get; private set; }

  public void Setup(string destinationPath) {
    DestinationPath = destinationPath;
  }

  public string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string fileNameWithoutExtension, string fallbackName, bool deleteExisting) {
    return Write(entries, sip, fileNameWithoutExtension, fallbackName, true, deleteExisting);
  }

  public string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string fileNameWithoutExtension, string fallbackName, bool createSipIdFolder, bool deleteExisting) {
    string zipPath = GetZipPath(DestinationPath, fileNameWithoutExtension, fallbackName);

    try {
      FileStream fileStream = new(zipPath, FileMode.Create);
      ZIPUtils.Zip(entries, fileStream, sip, createSipIdFolder, true);
      fileStream.Close();
    } catch (OperationCanceledException e) {
      throw new ThreadInterruptedException("Operation canceled.", e);
    } catch (IOException e) {
      throw new IPException("Error generating E-ARK SIP ZIP file. Reason: " + e.Message, e);
    }

    return zipPath;
  }

  private string GetZipPath(string destinationDirectory, string fileNameWithoutExtension, string fallbackName) {
    string zipPath;

    if (fileNameWithoutExtension != null) {
      zipPath = Path.Combine(destinationDirectory, fileNameWithoutExtension + IPConstants.SIP_FILE_EXTENSION);
    } else {
      zipPath = Path.Combine(destinationDirectory, fallbackName + IPConstants.SIP_FILE_EXTENSION);
    }

    try {
      if(File.Exists(zipPath)) File.Delete(zipPath);
    } catch (IOException e) {
      throw new IPException("Error deleting already existing zip", e);
    }

    return zipPath;
  }
}