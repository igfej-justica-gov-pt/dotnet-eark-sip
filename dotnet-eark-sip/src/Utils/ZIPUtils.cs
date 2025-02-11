using Mets;
using Microsoft.Extensions.Logging;
using System.IO.Compression;
using System.Security.Cryptography;

public static class ZIPUtils {
  private static readonly ILogger logger = DefaultLogger.Create("ZipUtils");

  public static Dictionary<string, IZipEntryInfo> AddMdRefFileToZip(Dictionary<string, IZipEntryInfo> zipEntries, string filePath, string zipPath, MdSecTypeMdRef mdRef) {
    zipEntries.Add(zipPath, new METSMdRefZipEntryInfo(zipPath, filePath, mdRef));
    return zipEntries;
  }

  public static Dictionary<string, IZipEntryInfo> AddFileTypeFileToZip(Dictionary<string, IZipEntryInfo> zipEntries, string filePath, string zipPath, FileType fileType) {
    zipEntries.Add(zipPath, new METSFileTypeZipEntryInfo(zipPath, filePath, fileType));
    return zipEntries;
  }

  public static Dictionary<string, IZipEntryInfo> AddMETSFileToZip(
    Dictionary<string, IZipEntryInfo> zipEntries,
    string filePath,
    string zipPath,
    Mets.Mets mets,
    bool mainMets,
    FileType fileType
  ) {
    zipEntries.Add(zipPath, new METSZipEntryInfo(zipPath, filePath, mets, mainMets, fileType));
    return zipEntries;
  }

  public static void Zip(Dictionary<string, IZipEntryInfo> files, FileStream outputStream, SIP sip, bool isCompressed) {
    Zip(files, outputStream, sip, true, isCompressed);
  }

  public static void Zip(Dictionary<string, IZipEntryInfo> files, FileStream outputStream, SIP sip, bool createSipIdFolder, bool isCompressed) {
    CompressionLevel compressionLevel = isCompressed ? CompressionLevel.Optimal : CompressionLevel.NoCompression;

    using (ZipArchive zipArchive = new(outputStream, ZipArchiveMode.Create, true)) {
      HashSet<IFilecoreChecksumtype> nonMetsChecksumAlgorithms = new() { sip.GetChecksumAlgorithm() };
      HashSet<IFilecoreChecksumtype> metsChecksumAlgorithms = new(nonMetsChecksumAlgorithms);
      metsChecksumAlgorithms.UnionWith(sip.GetExtraChecksumAlgorithms());

      int i = 0;
      foreach (IZipEntryInfo file in files.Values) {
        file.ChecksumAlgorithm = sip.GetChecksumAlgorithm();
        file.PrepareEntryForZipping();

        logger.LogDebug("Zipping {file}", file.FilePath);
        string entryName = createSipIdFolder ? $"{sip.GetId()}/{file.Name}" : file.Name;
        ZipArchiveEntry entry = zipArchive.CreateEntry(entryName, compressionLevel);

        try {
          using (Stream entryStream = entry.Open())
          using (FileStream inputStream = new(file.FilePath, FileMode.Open, FileAccess.Read)) {
            Dictionary<IFilecoreChecksumtype, string> checksums;

            if (file is METSZipEntryInfo metsEntry) {
              checksums = CalculateChecksums(entryStream, inputStream, metsChecksumAlgorithms);
              metsEntry.Checksums = checksums;
              metsEntry.Size = new FileInfo(metsEntry.FilePath).Length;
            } else {
              checksums = CalculateChecksums(entryStream, inputStream, nonMetsChecksumAlgorithms);
            }

            logger.LogDebug("Done zipping {file}", file.FilePath);
            IFilecoreChecksumtype checksumType = sip.GetChecksumAlgorithm();
            string checksum = checksums[checksumType];

            file.Checksum = checksum;
            file.ChecksumAlgorithm = checksumType;

            if (file is METSFileTypeZipEntryInfo f) {
              f.MetsFileType.Checksum = checksum;
              f.MetsFileType.Checksumtype = checksumType;
              f.MetsFileType.ChecksumtypeSpecified = true;
            } else if (file is METSMdRefZipEntryInfo md) {
              md.MetsMdRef.Checksum = checksum;
              md.MetsMdRef.Checksumtype = checksumType;
              md.MetsMdRef.ChecksumtypeSpecified = true;
            }
          }
        } catch (Exception e) {
          logger.LogError(e, "Error while zipping {file}", file.FilePath);
        }

        i++;
        sip.NotifySipBuildPackagingCurrentStatus(i);
      }
    }

    outputStream.Dispose();
  }

  public static Dictionary<IFilecoreChecksumtype, string> CalculateChecksums(Stream zipOutputStream, Stream stream, HashSet<IFilecoreChecksumtype> checksumAlgorithms) {
    byte[] buffer = new byte[4096];
    Dictionary<IFilecoreChecksumtype, string> result = new();

    Dictionary<IFilecoreChecksumtype, HashAlgorithm> algorithms = new();
    foreach (IFilecoreChecksumtype algorithm in checksumAlgorithms) {
      algorithms.Add(algorithm, HashAlgorithm.Create(Enum.GetName(typeof(IFilecoreChecksumtype), algorithm)));
    }

    int numRead;
    while ((numRead = stream.Read(buffer, 0, buffer.Length)) > 0) {
      foreach (KeyValuePair<IFilecoreChecksumtype, HashAlgorithm> algorithm in algorithms.AsEnumerable()) {
        algorithm.Value.TransformBlock(buffer, 0, numRead, buffer, 0);
      }

      zipOutputStream?.Write(buffer, 0, numRead);
    };

    foreach (KeyValuePair<IFilecoreChecksumtype, HashAlgorithm> algorithm in algorithms) {
      algorithm.Value.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
      result.Add(algorithm.Key, BitConverter.ToString(algorithm.Value.Hash).Replace("-", "").ToUpperInvariant());
    }

    return result;
  }
}