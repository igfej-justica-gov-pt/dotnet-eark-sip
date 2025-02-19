using CommandLine;
using Mets;

[Verb("create", true, HelpText = "Creates E-ARK SIP package")]
public class Options {
  [Option('p', "path", Required = false, HelpText = "Path where the E-ARK SIP should be saved")]
  public string Path { get; set; } = Directory.GetCurrentDirectory();
  [Option("submitter-name", Required = true, HelpText = "Submitter agent name")]
  public string SubmitterAgentName { get; set; } = "";
  [Option("submitter-id", Required = true, HelpText = "Submitter agent identifier")]
  public string SubmitterAgentId { get; set; } = "";
  [Option("sip-id", Required = true, HelpText = "E-ARK SIP identifier")]
  public string SIPId { get; set; } = "";

  [Option('T', "target-only", HelpText = "Adds only the files for the representations")]
  public bool TargetOnly { get; set; }
  [Option("override-schema", HelpText = "Overrides default schema")]
  public bool OverrideSchema { get; set; }

  [Option('a', "ancestors", Required = false, HelpText = "E-ARK SIP ancestors", Separator = ',')]
  public IEnumerable<string>? Ancestors { get; set; }
  [Option('d', "documentation", Required = false, HelpText = "Path(s) to documentation file(s)", Separator = ',')]
  public IEnumerable<string>? Documentation { get; set; }
  [Option("metadata-group", HelpText = "Start a new metadata group", Group = "required-files")]
  public IEnumerable<Metadata> MetadataListArgs { get; set; } = new List<Metadata>();
  [Option("representation-group", HelpText = "Start a new representation group", Group = "required-files")]
  public IEnumerable<Representation> RepresentationListArgs { get; set; } = new List<Representation>();


  [Option('v', "version", Required = false, HelpText = "E-ARK SIP specification version (possible values: ${COMPLETION-CANDIDATES})")]
  public CSIPVersion Version { get; set; } = CSIPVersion.V220;
  [Option('C', "checksum", Required = false, HelpText = "Checksum algorithms (possible values: ${COMPLETION-CANDIDATES})")]
  public IFilecoreChecksumtype ChecksumAlgorithm { get; set; } = IFilecoreChecksumtype.SHA256;
  [Option('s', "strategy", Required = false, HelpText = "Write strategy to be used (possible values: ${COMPLETION-CANDIDATES})")]
  public WriteStrategyEnum Strategy { get; set; } = WriteStrategyEnum.ZIP;
}