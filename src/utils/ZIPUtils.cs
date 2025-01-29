using System.Collections;
using System.Security.Cryptography;
using Mets;

public static class ZIPUtils {
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

  public static Dictionary<string, string> CalculateChecksums(Stream zipOutputStream, Stream stream, HashSet<string> checksumAlgorithms) {
    byte[] buffer = new byte[4096];
    Dictionary<string, string> result = new Dictionary<string, string>();

    Dictionary<string, HashAlgorithm> algorithms = new Dictionary<string, HashAlgorithm>();
    foreach (string algorithm in checksumAlgorithms) {
      algorithms.Add(algorithm, HashAlgorithm.Create(algorithm));
    }

    int numRead;
    do {
      numRead = stream.Read(buffer, 0, buffer.Length);
      if (numRead > 0) {
        foreach (KeyValuePair<string, HashAlgorithm> algorithm in algorithms.AsEnumerable()) {
          algorithm.Value.TransformFinalBlock(buffer, 0, numRead);
        }

        zipOutputStream?.Write(buffer, 0, numRead);
      }
    } while (numRead != -1);

    foreach (KeyValuePair<string, HashAlgorithm> algorithm in algorithms.AsEnumerable()) {
      result.Add(algorithm.Key, BitConverter.ToString(algorithm.Value.Hash).Replace("-", "").ToUpperInvariant());
    }

    return result;
  }
}