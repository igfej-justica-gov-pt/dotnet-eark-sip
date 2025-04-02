using Mets;

/// <summary>
/// Represents information about a file entry in a ZIP archive.
/// </summary>
public class FileZipEntryInfo : IZipEntryInfo
{
    /// <summary>
    /// Gets the name of the file entry.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the file path of the file entry.
    /// </summary>
    public string FilePath { get; private set; }

    /// <summary>
    /// Gets or sets the checksum of the file entry.
    /// </summary>
    public string? Checksum { get; set; }

    /// <summary>
    /// Gets or sets the checksum algorithm used for the file entry.
    /// </summary>
    public IFilecoreChecksumtype ChecksumAlgorithm { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileZipEntryInfo"/> class with the specified name and file path.
    /// </summary>
    /// <param name="name">The name of the file entry.</param>
    /// <param name="filePath">The file path of the file entry.</param>
    public FileZipEntryInfo(string name, string filePath)
    {
        Name = name;
        FilePath = filePath;
    }

    /// <summary>
    /// Prepares the file entry for inclusion in a ZIP archive.
    /// </summary>
    public virtual void PrepareEntryForZipping() { }
}