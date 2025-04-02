using Mets;

/// <summary>
/// Represents information about a METS file type zip entry, including its name, file path, and associated file type.
/// </summary>
public class METSFileTypeZipEntryInfo : FileZipEntryInfo
{
    /// <summary>
    /// Gets or sets the file type associated with the METS file.
    /// </summary>
    public FileType MetsFileType { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="METSFileTypeZipEntryInfo"/> class with the specified name and file path.
    /// </summary>
    /// <param name="name">The name of the zip entry.</param>
    /// <param name="filePath">The file path of the zip entry.</param>
    public METSFileTypeZipEntryInfo(string name, string filePath) : base(name, filePath)
    {
        MetsFileType = new FileType();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="METSFileTypeZipEntryInfo"/> class with the specified name, file path, and file type.
    /// </summary>
    /// <param name="name">The name of the zip entry.</param>
    /// <param name="filePath">The file path of the zip entry.</param>
    /// <param name="fileType">The file type associated with the METS file.</param>
    public METSFileTypeZipEntryInfo(string name, string filePath, FileType fileType) : base(name, filePath)
    {
        MetsFileType = fileType;
    }
}