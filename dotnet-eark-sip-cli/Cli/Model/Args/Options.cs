using CommandLine;
using Mets;

[Verb("create", isDefault: true, HelpText = "Creates E-ARK SIP package")]
public class Options {
  [Option("submitter-name", HelpText = "Submitter agent name")]
  public string SubmitterAgentName { get; set; } = "";

  [Option("submitter-id", HelpText = "Submitter agent identifier")]
  public string SubmitterAgentId { get; set; } = "";

  [Option("sip-id", HelpText = "E-ARK SIP identifier")]
  public string SIPId { get; set; } = "";

  [Option('p', "path", HelpText = "Path where the E-ARK SIP should be saved")]
  public string Path { get; set; } = Directory.GetCurrentDirectory();


  [Option('T', "target-only", HelpText = "Adds only the files for the representations")]
  public bool TargetOnly { get; set; }

  [Option("override-schema", HelpText = "Overrides default schema")]
  public bool OverrideSchema { get; set; }


  [Option('a', "ancestors", HelpText = "E-ARK SIP ancestors", Separator = ',')]
  public IEnumerable<string>? Ancestors { get; set; }

  [Option('d', "documentation", HelpText = "Path(s) to documentation file(s)", Separator = ',')]
  public IEnumerable<string>? Documentation { get; set; }


  [Option('v', "version", HelpText = "E-ARK SIP specification version (possible values: ${COMPLETION-CANDIDATES})")]
  public string Version { get; set; } = EnumUtils.GetXmlEnumName(CSIPVersion.V220);

  [Option('C', "checksum", HelpText = "Checksum algorithms (possible values: ${COMPLETION-CANDIDATES})")]
  public string ChecksumAlgorithm { get; set; } = EnumUtils.GetXmlEnumName(IFilecoreChecksumtype.SHA256);

  [Option('s', "strategy", HelpText = "Write strategy to be used (possible values: ${COMPLETION-CANDIDATES})")]
  public string Strategy { get; set; } = EnumUtils.GetXmlEnumName(WriteStrategyEnum.ZIP);


  // Metadata section
  [Option("metadata-files", Separator = ',', HelpText = "Path to descriptive metadata file (comma-separated for different representations)")]
  public IEnumerable<string> MetadataFiles { get; set; } = new List<string>();

  [Option("metadata-types", Separator = ',', HelpText = "Descriptive metadata type (comma-separated for different representations)")]
  public IEnumerable<string> MetadataTypes { get; set; } = new List<string>();

  [Option("metadata-versions", Separator = ',', HelpText = "Descriptive metadata version (comma-separated for different representations)")]
  public IEnumerable<string> MetadataVersions { get; set; } = new List<string>();

  [Option("metadata-schemas", Separator = ',', HelpText = "Path to descriptive metadata schema file (comma-separated for different representations)")]
  public IEnumerable<string> MetadataSchemas { get; set; } = new List<string>();

  public List<Metadata> GetMetadataFiles() {
    List<Metadata> metadataFiles = new();

    for (int i = 0; i < MetadataFiles.Count(); i++) {
      string metadataFile = MetadataFiles.ElementAt(i);
      string metadataType = MetadataTypes.ElementAt(i);
      string? metadataVersion = MetadataVersions.Count() > 0 ? MetadataTypes.ElementAt(i) : null;
      string? metadataSchemas = MetadataSchemas.Count() > 0 ? MetadataSchemas.ElementAt(i) : null;

      metadataFiles.Add(new Metadata(metadataFile, metadataType, metadataVersion, metadataSchemas));
    }

    return metadataFiles;
  }


  // Representation section
  [Option(
    "representation-data-lists",
    Separator = ';',
    HelpText = "Path(s) to representation files (comma-separated between files, semicolon-separated between representations)"
  )]
	public IEnumerable<string> RepresentationDataLists { get; set; } = new List<string>();

	[Option("representation-types", Separator = ',', HelpText = "Representation types (comma-separated for different representations)")]
	public IEnumerable<string> RepresentationTypes { get; set; } = new List<string>();

	[Option("representation-ids", Separator = ',', HelpText = "Representation identifier (comma-separated for different representations)")]
	public IEnumerable<string> RepresentationIds { get; set; } = new List<string>();

  public List<Representation> GetRepresentations() {
    List<Representation> representations = new();

    for (int i = 0; i < RepresentationDataLists.Count(); i++) {
      List<string> representationData = RepresentationDataLists.ElementAt(i).Split(',').ToList();
      string? representationType = RepresentationTypes.Count() > 0 ? RepresentationTypes.ElementAt(i) : null;
      string? representationId = RepresentationIds.Count() > 0 ? RepresentationIds.ElementAt(i) : null;

      representations.Add(new Representation(representationData, representationType, representationId));
    }

    return representations;
  }
}