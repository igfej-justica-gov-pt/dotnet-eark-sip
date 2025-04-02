using Mets;
using Microsoft.Extensions.Logging;
using System.IO.Compression;
using System.Security.Cryptography;

/// <summary>
/// Provides utility methods for working with ZIP files, including adding files and calculating checksums.
/// </summary>
public static class ZIPUtils
{
    private static readonly ILogger logger = DefaultLogger.Create("ZipUtils");

    /// <summary>
    /// Adds a metadata reference file to the ZIP entries.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="filePath">The file path of the metadata reference file.</param>
    /// <param name="zipPath">The path within the ZIP archive where the file will be added.</param>
    /// <param name="mdRef">The metadata reference information.</param>
    /// <returns>The updated dictionary of ZIP entries.</returns>
    public static Dictionary<string, IZipEntryInfo> AddMdRefFileToZip(Dictionary<string, IZipEntryInfo> zipEntries, string filePath, string zipPath, MdSecTypeMdRef mdRef)
    {
        zipEntries.Add(zipPath, new METSMdRefZipEntryInfo(zipPath, filePath, mdRef));
        return zipEntries;
    }

    /// <summary>
    /// Adds a file to the ZIP entries.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="filePath">The file path of the file type file.</param>
    /// <param name="zipPath">The path within the ZIP archive where the file will be added.</param>
    /// <param name="fileType">The file type information.</param>
    /// <returns>The updated dictionary of ZIP entries.</returns>
    public static Dictionary<string, IZipEntryInfo> AddFileTypeFileToZip(Dictionary<string, IZipEntryInfo> zipEntries, string filePath, string zipPath, FileType fileType)
    {
        zipEntries.Add(zipPath, new METSFileTypeZipEntryInfo(zipPath, filePath, fileType));
        return zipEntries;
    }

    /// <summary>
    /// Adds a METS file to the ZIP entries.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="filePath">The file path of the METS file.</param>
    /// <param name="zipPath">The path within the ZIP archive where the file will be added.</param>
    /// <param name="mets">The METS object containing metadata.</param>
    /// <param name="mainMets">Indicates whether this is the main METS file.</param>
    /// <param name="fileType">The optional file type information.</param>
    /// <returns>The updated dictionary of ZIP entries.</returns>
    public static Dictionary<string, IZipEntryInfo> AddMETSFileToZip(
      Dictionary<string, IZipEntryInfo> zipEntries,
      string filePath,
      string zipPath,
      Mets.Mets mets,
      bool mainMets,
      FileType? fileType
    )
    {
        zipEntries.Add(zipPath, new METSZipEntryInfo(zipPath, filePath, mets, mainMets, fileType));
        return zipEntries;
    }

    /// <summary>
    /// Creates a ZIP archive from the provided files and writes it to the specified output stream.
    /// </summary>
    /// <param name="files">The dictionary of files to include in the ZIP archive.</param>
    /// <param name="outputStream">The output stream where the ZIP archive will be written.</param>
    /// <param name="sip">The SIP object containing metadata and checksum algorithms.</param>
    /// <param name="isCompressed">Indicates whether the ZIP archive should be compressed.</param>
    /// <remarks>
    /// Creates a folder with the SIP ID in the ZIP archive.
    /// </remarks>
    public static void Zip(Dictionary<string, IZipEntryInfo> files, FileStream outputStream, SIP sip, bool isCompressed)
    {
        Zip(files, outputStream, sip, true, isCompressed);
    }

    /// <summary>
    /// Creates a ZIP archive from the provided files and writes it to the specified output stream.
    /// </summary>
    /// <param name="files">The dictionary of files to include in the ZIP archive.</param>
    /// <param name="outputStream">The output stream where the ZIP archive will be written.</param>
    /// <param name="sip">The SIP object containing metadata and checksum algorithms.</param>
    /// <param name="createSipIdFolder">Indicates whether to create a folder with the SIP ID in the ZIP archive.</param>
    /// <param name="isCompressed">Indicates whether the ZIP archive should be compressed.</param>
    public static void Zip(Dictionary<string, IZipEntryInfo> files, FileStream outputStream, SIP sip, bool createSipIdFolder, bool isCompressed)
    {
        CompressionLevel compressionLevel = isCompressed ? CompressionLevel.Optimal : CompressionLevel.NoCompression;

        using (ZipArchive zipArchive = new(outputStream, ZipArchiveMode.Create, true))
        {
            HashSet<IFilecoreChecksumtype> nonMetsChecksumAlgorithms = new() { sip.GetChecksumAlgorithm() };
            HashSet<IFilecoreChecksumtype> metsChecksumAlgorithms = new(nonMetsChecksumAlgorithms);
            metsChecksumAlgorithms.UnionWith(sip.GetExtraChecksumAlgorithms());

            int i = 0;
            foreach (IZipEntryInfo file in files.Values)
            {
                file.ChecksumAlgorithm = sip.GetChecksumAlgorithm();
                file.PrepareEntryForZipping();

                logger.LogDebug("Zipping {file}", file.FilePath);
                string entryName = createSipIdFolder ? $"{sip.GetId()}/{file.Name}" : file.Name;
                ZipArchiveEntry entry = zipArchive.CreateEntry(entryName, compressionLevel);

                try
                {
                    using (Stream entryStream = entry.Open())
                    using (FileStream inputStream = new(file.FilePath, FileMode.Open, FileAccess.Read))
                    {
                        Dictionary<IFilecoreChecksumtype, string> checksums;

                        if (file is METSZipEntryInfo metsEntry)
                        {
                            checksums = CalculateChecksums(entryStream, inputStream, metsChecksumAlgorithms);
                            metsEntry.Checksums = checksums;
                            metsEntry.Size = new FileInfo(metsEntry.FilePath).Length;
                        }
                        else
                        {
                            checksums = CalculateChecksums(entryStream, inputStream, nonMetsChecksumAlgorithms);
                        }

                        logger.LogDebug("Done zipping {file}", file.FilePath);
                        IFilecoreChecksumtype checksumType = sip.GetChecksumAlgorithm();
                        string checksum = checksums[checksumType];

                        file.Checksum = checksum;
                        file.ChecksumAlgorithm = checksumType;

                        if (file is METSFileTypeZipEntryInfo f)
                        {
                            f.MetsFileType.Checksum = checksum;
                            f.MetsFileType.Checksumtype = checksumType;
                            f.MetsFileType.ChecksumtypeSpecified = true;
                        }
                        else if (file is METSMdRefZipEntryInfo md)
                        {
                            md.MetsMdRef.Checksum = checksum;
                            md.MetsMdRef.Checksumtype = checksumType;
                            md.MetsMdRef.ChecksumtypeSpecified = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.LogError(e, "Error while zipping {file}", file.FilePath);
                }

                i++;
                sip.NotifySipBuildPackagingCurrentStatus(i);
            }
        }

        outputStream.Dispose();
    }

    /// <summary>
    /// Calculates checksums for a given stream using specified checksum algorithms.
    /// </summary>
    /// <param name="zipOutputStream">The optional output stream for the ZIP archive.</param>
    /// <param name="stream">The input stream to calculate checksums for.</param>
    /// <param name="checksumAlgorithms">The set of checksum algorithms to use.</param>
    /// <returns>A dictionary mapping each checksum algorithm to its calculated checksum value.</returns>
    public static Dictionary<IFilecoreChecksumtype, string> CalculateChecksums(Stream? zipOutputStream, Stream stream, HashSet<IFilecoreChecksumtype> checksumAlgorithms)
    {
        byte[] buffer = new byte[4096];
        Dictionary<IFilecoreChecksumtype, string> result = new();

        Dictionary<IFilecoreChecksumtype, HashAlgorithm> algorithms = new();
        foreach (IFilecoreChecksumtype algorithm in checksumAlgorithms)
        {
            algorithms.Add(algorithm, HashAlgorithm.Create(EnumUtils.GetXmlEnumName(algorithm)));
        }

        int numRead;
        while ((numRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            foreach (KeyValuePair<IFilecoreChecksumtype, HashAlgorithm> algorithm in algorithms.AsEnumerable())
            {
                algorithm.Value.TransformBlock(buffer, 0, numRead, buffer, 0);
            }

            zipOutputStream?.Write(buffer, 0, numRead);
        }
      ;

        foreach (KeyValuePair<IFilecoreChecksumtype, HashAlgorithm> algorithm in algorithms)
        {
            algorithm.Value.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
            result.Add(algorithm.Key, BitConverter.ToString(algorithm.Value.Hash).Replace("-", "").ToUpperInvariant());
        }

        return result;
    }
}