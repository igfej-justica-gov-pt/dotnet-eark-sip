using Mets;
using Microsoft.Extensions.Logging;

/// <summary>
/// Represents information about a METS zip entry, including its checksums, size, and file type.
/// </summary>
public class METSZipEntryInfo : FileZipEntryInfo
{
    private static readonly ILogger<METSZipEntryInfo> logger = DefaultLogger.Create<METSZipEntryInfo>();

    private readonly Mets.Mets mets;
    private readonly bool mainMets;
    /// <summary>
    /// Gets or sets the checksums associated with the METS zip entry.
    /// </summary>
    public Dictionary<IFilecoreChecksumtype, string> Checksums { get; set; }

    /// <summary>
    /// Gets or sets the size of the METS zip entry.
    /// </summary>
    public long Size { get; set; }
    private readonly FileType? fileType;

    /// <summary>
    /// Initializes a new instance of the <see cref="METSZipEntryInfo"/> class.
    /// </summary>
    /// <param name="name">The name of the zip entry.</param>
    /// <param name="filePath">The file path of the zip entry.</param>
    /// <param name="mets">The METS object associated with the zip entry.</param>
    /// <param name="mainMets">Indicates whether this is the main METS file.</param>
    /// <param name="fileType">The file type of the zip entry, if applicable.</param>
    public METSZipEntryInfo(string name, string filePath, Mets.Mets mets, bool mainMets, FileType? fileType) : base(name, filePath)
    {
        this.mets = mets;
        this.mainMets = mainMets;
        Checksums = new Dictionary<IFilecoreChecksumtype, string>();
        Size = 0;
        this.fileType = fileType;
    }

    /// <summary>
    /// Prepares the METS zip entry for zipping by marshalling the METS object and setting file information.
    /// </summary>
    /// <exception cref="IPException">Thrown when an error occurs during the marshalling of METS.</exception>
    public override void PrepareEntryForZipping()
    {
        try
        {
            METSUtils.MarshallMETS(mets, FilePath, mainMets);

            if (!mainMets && fileType != null)
            {
                METSUtils.SetFileBasicInformation(logger, FilePath, fileType);

                IFilecoreChecksumtype checksumType = ChecksumAlgorithm;
                HashSet<IFilecoreChecksumtype> checksumAlgorithms = new HashSet<IFilecoreChecksumtype> { checksumType };
                using (FileStream inputStream = File.Open(FilePath, FileMode.Open, FileAccess.Read))
                {
                    Dictionary<IFilecoreChecksumtype, string> checksums = ZIPUtils.CalculateChecksums(null, inputStream, checksumAlgorithms);

                    fileType.Checksum = checksums[checksumType];
                    fileType.Checksumtype = checksumType;
                    fileType.ChecksumtypeSpecified = true;
                }
            }
        }
        catch (Exception e)
        {
            throw new IPException("Error marshalling METS", e);
        }
    }
}