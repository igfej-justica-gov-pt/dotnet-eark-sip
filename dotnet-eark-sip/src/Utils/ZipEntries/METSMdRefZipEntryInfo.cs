using Mets;

/// <summary>
/// Represents information about a METS MdRef zip entry, including its name, file path, and associated METS MdRef.
/// </summary>
public class METSMdRefZipEntryInfo : FileZipEntryInfo
{
    /// <summary>
    /// Gets or sets the METS MdRef associated with the zip entry.
    /// </summary>
    public MdSecTypeMdRef MetsMdRef { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="METSMdRefZipEntryInfo"/> class with the specified name and file path.
    /// </summary>
    /// <param name="name">The name of the zip entry.</param>
    /// <param name="filePath">The file path of the zip entry.</param>
    public METSMdRefZipEntryInfo(string name, string filePath) : base(name, filePath)
    {
        MetsMdRef = new MdSecTypeMdRef();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="METSMdRefZipEntryInfo"/> class with the specified name, file path, and METS MdRef.
    /// </summary>
    /// <param name="name">The name of the zip entry.</param>
    /// <param name="filePath">The file path of the zip entry.</param>
    /// <param name="mdRef">The METS MdRef associated with the zip entry.</param>
    public METSMdRefZipEntryInfo(string name, string filePath, MdSecTypeMdRef mdRef) : base(name, filePath)
    {
        MetsMdRef = mdRef;
    }
}