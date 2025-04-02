using Mets;

/// <summary>
/// Represents information about a zip entry, including its name, file path, checksum, and preparation for zipping.
/// </summary>
public interface IZipEntryInfo
{
    /// <summary>
    /// Gets the name of the zip entry.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the file path of the zip entry.
    /// </summary>
    string FilePath { get; }

    /// <summary>
    /// Gets or sets the checksum of the zip entry.
    /// </summary>
    string? Checksum { get; set; }

    /// <summary>
    /// Gets or sets the checksum algorithm used for the zip entry.
    /// </summary>
    IFilecoreChecksumtype ChecksumAlgorithm { get; set; }

    /// <summary>
    /// Prepares the entry for zipping.
    /// </summary>
    void PrepareEntryForZipping();
}