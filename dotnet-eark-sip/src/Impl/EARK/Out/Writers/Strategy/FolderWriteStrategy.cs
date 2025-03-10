using System.Security.Cryptography;
using Mets;
using Microsoft.Extensions.Logging;

public class FolderWriteStrategy : IWriteStrategy {
  private static readonly ILogger logger = DefaultLogger.Create<FolderWriteStrategy>();

  public string DestinationPath { get; private set; } = "";

  public void Setup(string destinationPath) {
    DestinationPath = destinationPath;
    if (!Directory.Exists(destinationPath)) Directory.CreateDirectory(destinationPath);
  }

  public string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string? fileNameWithoutExtension, string fallbackName, bool deleteExisting) {
    string dirPath = GetDirPath(DestinationPath, fileNameWithoutExtension, fallbackName, deleteExisting);

    WriteToPath(entries, dirPath, sip.GetChecksumAlgorithm());

    return dirPath;
  }

  public string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string? fileNameWithoutExtension, string fallbackName, bool deleteExisting, bool createSipIdFolder) {
    throw new NotImplementedException();
  }

  private void WriteToPath(Dictionary<string, IZipEntryInfo> entries, string path, IFilecoreChecksumtype checksumAlgorithm) {
    try {
      Directory.CreateDirectory(path);

      foreach (IZipEntryInfo zipEntryInfo in entries.Values) {
        zipEntryInfo.ChecksumAlgorithm = checksumAlgorithm;
        zipEntryInfo.PrepareEntryForZipping();
        logger.LogDebug("Writing to {file}", zipEntryInfo.FilePath);

        string outputPath = Path.Combine(path, zipEntryInfo.Name);
        WriteFileToPath(zipEntryInfo, outputPath, checksumAlgorithm);
      }
    } catch (IOException e) {
      logger.LogDebug(e, "Error in write method");
      throw new IPException(e.Message, e);
    }
  }

  private void WriteFileToPath(IZipEntryInfo zipEntryInfo, string outputPath, IFilecoreChecksumtype checksumAlgorithm) {
    Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

    using (FileStream inputStream = new(zipEntryInfo.FilePath, FileMode.Open, FileAccess.Read))
    using (FileStream outputStream = new(outputPath, FileMode.Create, FileAccess.Write))
    using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(Enum.GetName(typeof(IFilecoreChecksumtype), checksumAlgorithm))) {
      byte[] buffer = new byte[4096];
      int bytesRead;

      while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0) {
        hashAlgorithm.TransformBlock(buffer, 0, bytesRead, buffer, 0);
        outputStream.Write(buffer, 0, bytesRead);
      }

      hashAlgorithm.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
      string checksum = BitConverter.ToString(hashAlgorithm.Hash).Replace("-", "").ToUpperInvariant();

      zipEntryInfo.Checksum = checksum;
      zipEntryInfo.ChecksumAlgorithm = checksumAlgorithm;
    }
  }

  private string GetDirPath(string targetPath, string? name, string fallbackName, bool deleteExisting) {
    string path = Path.Combine(targetPath, name ?? fallbackName);

    try {
      if (deleteExisting) DeleteFilesFromDirectory(path);
    } catch (IOException e) {
      throw new IPException("Error deleting existing path - " + e.Message, e);
    }

    return path;
  }

  private void DeleteFilesFromDirectory(string path) {
    foreach (string file in Directory.GetFiles(path)) {
      File.Delete(file);
    }
  }
}