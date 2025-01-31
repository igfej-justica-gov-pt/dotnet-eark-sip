using Mets;

public class MetsWrapper {
  public Mets.Mets Mets { get; set; }
  public string MetsPath { get; set; }

  public DivType MainDiv { get; set; }
  public DivType MetadataDiv { get; set; }
  public DivType OtherMetadataDiv { get; set; }
  public DivType DataDiv { get; set; }
  public DivType SchemasDiv { get; set; }
  public DivType SubmissionsDiv { get; set; }
  public DivType DocumentationDiv { get; set; }

  public FileGrpType MainFileGroup { get; set; }

  public FileGrpType SchemasFileGroup { get; set; }
  public FileGrpType SubmissionFileGroup { get; set; }
  public FileGrpType DocumentationFileGroup { get; set; }
  public FileGrpType DataFileGroup { get; set; }

  public MdSecType MainDmdSec { get; set; }
  public MdSecType DocumentationDmdSec { get; set; }

  public MetsWrapper(Mets.Mets mets, string metsPath) : base() {
    Mets = mets;
    MetsPath = metsPath;
  }
}