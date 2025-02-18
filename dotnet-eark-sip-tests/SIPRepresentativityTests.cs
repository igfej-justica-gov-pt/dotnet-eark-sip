using IP;
using Mets;

namespace dotnet_eark_sip_tests;

[Collection("Non-Parallel Tests")]
public class SIPRepresentativityTests : IDisposable {
  private readonly string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "representativity_tests");

	/// <summary>
	/// Test initialization: create output directory
	/// </summary>
	public SIPRepresentativityTests() {
		Directory.CreateDirectory(outputPath);
	}

	/// <summary>
	/// Tests cleanup: delete temp files and directory
	/// </summary>
  public void Dispose() {
    Utils.DeleteDirectory(outputPath);
  }

  [Fact]
  public void Create_FullEARKSIPWithFolders_CreatesZipWithCorrectFiles() {
    string zipPath = CreateFullEARKSIPWithFolders();
    Assert.True(File.Exists(zipPath));

    // TODO: Add additional verifications
  }

  /// <summary>
  /// Creates a full EARKSIP to be tested.
  /// </summary>
  /// <returns>
  /// Path to the generated EARKSIP zip.
  /// </returns>
  private string CreateFullEARKSIPWithFolders() {
    // 1) instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.1.0");
    sip.AddCreatorSoftwareAgent("KEEPS .NET E-ARK SIP", "1.0.0");

    // 1.1) set optional human-readable description
    sip.SetDescription("A full E-ARK SIP");

    // 1.2) add descriptive metadata (SIP level)
    IPDescriptiveMetadata descriptiveMetadata = new IPDescriptiveMetadata(
      new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\metadata_descriptive_dc.xml")),
      new MetadataType(IMetadataMdtype.DC),
      null
    );
    sip.AddDescriptiveMetadata(descriptiveMetadata);

    // 1.3) add preservation metadata (SIP level)
    IPMetadata metadataPreservation = new IPMetadata(
      new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\metadata_preservation_premis.xml"))
    ).SetMetadataType(IMetadataMdtype.PREMIS);
    sip.AddPreservationMetadata(metadataPreservation);

    // 1.4) add other metadata (SIP level)
    IPFile metadataOtherFile = new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\metadata_other.txt"));
    // 1.4.1) optionally one may rename file final name
    metadataOtherFile.SetRenameTo("metadata_other_renamed.txt");
    IPMetadata metadataOther = new IPMetadata(metadataOtherFile);
    sip.AddOtherMetadata(metadataOther);
    // 1.4.1) optionally one may rename file final name
    metadataOtherFile = new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\metadata_other.txt"));
    metadataOtherFile.SetRenameTo("metadata_other_renamed2.txt");
    metadataOther = new IPMetadata(metadataOtherFile);
    sip.AddOtherMetadata(metadataOther);

    // 1.5) add xml schema (SIP level)
    sip.AddSchema(new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\schema.xsd")));

    // 1.6) add documentation (SIP level)
    sip.AddDocumentation(new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\documentation.pdf")));

    // 1.7) set optional RODA related information about ancestors
    sip.SetAncestors(["b6f24059-8973-4582-932d-eb0b2cb48f28"]);

    // 1.8) add an agent (SIP level)
    sip.AddAgent(new IPAgent(
      "Agent Name",
      MetsTypeMetsHdrAgentRole.OTHER,
      "OTHER ROLE",
      MetsTypeMetsHdrAgentType.INDIVIDUAL,
      "OTHER TYPE",
      "",
      Xml.Mets.CsipExtensionMets.Notetype.SOFTWARE_VERSION
    ));

    // 1.9) Add a representation (status will be set to the default value, i.e. ORIGINAL)
    IPRepresentation representation1 = new("representation 1");
    sip.AddRepresentation(representation1);
    RepresentationUtils.IncludeInRepresentation(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\Representative"), representation1);

    // 2) build SIP, providing an output directory
    IWriteStrategy writeStrategy = SIPBuilderUtils.GetWriteStrategy(WriteStrategyEnum.ZIP, outputPath);
    string zipSIP = sip.Build(writeStrategy);

    return zipSIP;
  }
}