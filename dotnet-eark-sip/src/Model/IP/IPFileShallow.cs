using IP;
using Mets;

/// <summary>
/// Represents a shallow implementation of an IP file with basic properties and methods.
/// </summary>
public class IPFileShallow : IIPFile
{
    /// <summary>
    /// Gets or sets the location of the file as a URI.
    /// </summary>
    public Uri FileLocation { get; set; }

    /// <summary>
    /// Gets or sets the type of the file.
    /// </summary>
    public FileType FileType { get; set; }
    private List<string> relativeFolders = new List<string>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    /// <summary>
    /// Initializes a new instance of the <see cref="IPFileShallow"/> class with the specified relative folders.
    /// </summary>
    /// <param name="relativeFolders">The list of relative folder paths.</param>
    public IPFileShallow(List<string> relativeFolders)
    {
        this.relativeFolders = relativeFolders;
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    /// <summary>
    /// Initializes a new instance of the <see cref="IPFileShallow"/> class with the specified file location and file type.
    /// </summary>
    /// <param name="fileLocation">The URI location of the file.</param>
    /// <param name="fileType">The type of the file.</param>
    public IPFileShallow(Uri fileLocation, FileType fileType) : base()
    {
        FileLocation = fileLocation;
        FileType = fileType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPFileShallow"/> class with the specified file location, file type, and relative folders.
    /// </summary>
    /// <param name="fileLocation">The URI location of the file.</param>
    /// <param name="fileType">The type of the file.</param>
    /// <param name="relativeFolders">The list of relative folder paths.</param>
    public IPFileShallow(Uri fileLocation, FileType fileType, List<string> relativeFolders) : base()
    {
        FileLocation = fileLocation;
        FileType = fileType;
        this.relativeFolders = relativeFolders;
    }

    public List<string> GetRelativeFolders()
    {
        return relativeFolders;
    }

    public IIPFile SetRelativeFolders(List<string> relativeFolders)
    {
        this.relativeFolders = relativeFolders;
        return this;
    }

    /// <summary>
    /// Creates an empty folder representation with the specified relative folder path.
    /// </summary>
    /// <param name="emptyFolderPath">The list of relative folder paths representing the empty folder.</param>
    /// <returns>A new instance of <see cref="IPFileShallow"/> representing the empty folder.</returns>
    public static IPFileShallow CreateEmptyFolder(List<string> emptyFolderPath)
    {
        return new IPFileShallow(emptyFolderPath);
    }

    public string? GetFileName() => null;

    public string GetPath()
    {
        throw new NotImplementedException("IPFileShallow does not support this method");
    }

    public override string ToString()
    {
        return "IPFileShallow[" +
          "FileLocation=" + FileLocation ?? " " +
          ", FileType=" + FileType +
          ", RelativeFolders=[" + string.Join(", ", relativeFolders) + "]" +
        "]";
    }
}