using Mets;

/// <summary>
/// Represents a wrapper for the METS (Metadata Encoding and Transmission Standard) object,
/// providing properties for various divisions and file groups.
/// </summary>
public class MetsWrapper
{
    /// <summary>
    /// Gets or sets the METS (Metadata Encoding and Transmission Standard) object.
    /// </summary>
    public Mets.Mets Mets { get; set; }

    /// <summary>
    /// Gets or sets the file path to the METS document.
    /// </summary>
    public string MetsPath { get; set; }

    /// <summary>
    /// Gets or sets the main division of the METS document.
    /// </summary>
    public DivType MainDiv { get; set; }
    /// <summary>
    /// Gets or sets the metadata division of the METS document.
    /// </summary>
    public DivType MetadataDiv { get; set; }
    /// <summary>
    /// Gets or sets the other metadata division of the METS document.
    /// </summary>
    public DivType OtherMetadataDiv { get; set; }
    /// <summary>
    /// Gets or sets the data division of the METS document.
    /// </summary>
    public DivType DataDiv { get; set; }
    /// <summary>
    /// Gets or sets the schemas division of the METS document.
    /// </summary>
    public DivType SchemasDiv { get; set; }
    /// <summary>
    /// Gets or sets the submissions division of the METS document.
    /// </summary>
    public DivType SubmissionsDiv { get; set; }
    /// <summary>
    /// Gets or sets the documentation division of the METS document.
    /// </summary>
    public DivType DocumentationDiv { get; set; }

    /// <summary>
    /// Gets or sets the main file group of the METS document.
    /// </summary>
    public FileGrpType MainFileGroup { get; set; }
    /// <summary>
    /// Gets or sets the schemas file group of the METS document.
    /// </summary>
    public FileGrpType SchemasFileGroup { get; set; }
    /// <summary>
    /// Gets or sets the submission file group of the METS document.
    /// </summary>
    public FileGrpType SubmissionFileGroup { get; set; }
    /// <summary>
    /// Gets or sets the documentation file group of the METS document.
    /// </summary>
    public FileGrpType DocumentationFileGroup { get; set; }
    /// <summary>
    /// Gets or sets the data file group of the METS document.
    /// </summary>
    public FileGrpType DataFileGroup { get; set; }

    /// <summary>
    /// Gets or sets the main descriptive metadata section of the METS document.
    /// </summary>
    public MdSecType MainDmdSec { get; set; }

    /// <summary>
    /// Gets or sets the documentation descriptive metadata section of the METS document.
    /// </summary>
    public MdSecType DocumentationDmdSec { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    /// <summary>
    /// Initializes a new instance of the <see cref="MetsWrapper"/> class with the specified METS object and file path.
    /// </summary>
    /// <param name="mets">The METS object to wrap.</param>
    /// <param name="metsPath">The file path to the METS document. If null, the current directory is used.</param>
    public MetsWrapper(Mets.Mets mets, string? metsPath) : base()
    {
        Mets = mets;
        MetsPath = metsPath ?? Directory.GetCurrentDirectory();
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}