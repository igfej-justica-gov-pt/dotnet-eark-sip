using IP;
using Mets;

namespace dotnet_eark_sip_tests;

public class EARKSIPTests : IDisposable {
  private readonly string REPRESENTATION_STATUS_NORMALIZED = "NORMALIZED";
  private readonly string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "earksip_tests");

	public EARKSIPTests() {
		Directory.CreateDirectory(outputPath);
	}

  public void Dispose() {
    // Utils.DeleteDirectory(tempFolder);
  }

  [Fact]
  public void Create_FullEARKSIP_CreatesZipWithCorrectFiles() {
    string zipPath = CreateFullEARKSIPForTestCompliance();
    Assert.True(File.Exists(zipPath));

    // TODO: Add additional verifications
  }

  private string CreateFullEARKSIPForTestCompliance() {
    // 1) instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.1.0");
    sip.AddCreatorSoftwareAgent("KEEPS .NET E-ARK SIP", "1.0.0");

    // 1.1) set optional human-readable description
    sip.SetDescription("A full E-ARK SIP");

    // 1.2) add descriptive metadata (SIP level)
    IPDescriptiveMetadata descriptiveMetadata = new(
      new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\metadata_descriptive_dc.xml")),
      new MetadataType(IMetadataMdtype.DC),
      null
    );
    sip.AddDescriptiveMetadata(descriptiveMetadata);

    // 1.3) add preservation metadata (SIP level)
    IPMetadata metadataPreservation = new(new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\metadata_preservation_premis.xml")));
    metadataPreservation.SetMetadataType(IMetadataMdtype.PREMIS);
    sip.AddPreservationMetadata(metadataPreservation);

    // 1.4) add other metadata (SIP level)
    IPFile metadataOtherFile = new(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\metadata_other.txt"));
    // 1.4.1) optionally one may rename file final name
    metadataOtherFile.SetRenameTo("metadata_other_renamed.txt");
    IPMetadata metadataOther = new(metadataOtherFile);
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
      MetsTypeMetsHdrAgentRole.CREATOR,
      "",
      MetsTypeMetsHdrAgentType.INDIVIDUAL,
      "OTHER TYPE",
      "",
      Xml.Mets.CsipExtensionMets.Notetype.IDENTIFICATIONCODE
    ));

    // 1.9) Add a representation (status will be set to the default value, i.e. ORIGINAL)
    IPRepresentation representation1 = new("representation 1");
    sip.AddRepresentation(representation1);

    // 1.9.1) Add a file to the representation
    IPFile representationFile = new(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\documentation.pdf"));
    representationFile.SetRenameTo("data_.pdf");
    representation1.AddFile(representationFile);

    // SIDE TEST: encoding
    if (!Utils.IsWindowsSystem()) {
      IPFile representationFileEnc1 = new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\documentation.pdf"));
      representationFileEnc1.SetRenameTo("enc1_\u0001\u001F.pdf");
      representation1.AddFile(representationFileEnc1);
    }

    IPFile representationFileEnc2 = new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\documentation.pdf"));
    representationFileEnc2.SetRenameTo("enc2_\u0080\u0081\u0090\u00FF.pdf");
    representation1.AddFile(representationFileEnc2);

    IPFile representationFileEnc3 = new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\documentation.pdf"));
    representationFileEnc3.SetRenameTo(Utils.IsWindowsSystem() ? "enc3_;@=&.pdf" : "enc3_;?:@=&.pdf");
    representation1.AddFile(representationFileEnc3);

    IPFile representationFileEnc4 = new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\documentation.pdf"));
    representationFileEnc4
      .SetRenameTo(Utils.IsWindowsSystem() ? "enc4_#%{}\\^~[ ]`.pdf" : "enc4_\"<>#%{}|\\^~[ ]`.pdf");
    representation1.AddFile(representationFileEnc4);

    IPFile representationFileEnc5 = new IPFile(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\documentation.pdf"));
    representationFileEnc5
      .SetRenameTo(Utils.IsWindowsSystem() ? "enc4_#+%{}\\^~[ ]`.pdf" : "enc4_\"<>+#%{}|\\^~[ ]`.pdf");
    representation1.AddFile(representationFileEnc5);

    // 1.9.2) add a file to the representation and put it inside a folder
    // called 'abc' which has a folder inside called 'def'
    IPFile representationFile2 = new(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\documentation.pdf"));
    representationFile2.SetRelativeFolders(["abc", "def"]);
    representation1.AddFile(representationFile2);

    // 1.10) add a representation & define its status
    IPRepresentation representation2 = new("representation 2");
    representation2.SetStatus(new RepresentationStatus(REPRESENTATION_STATUS_NORMALIZED));
    sip.AddRepresentation(representation2);

    // 1.10.1) add a file to the representation
    IPFile representationFile3 = new(Path.Combine(Directory.GetCurrentDirectory(), ".\\Resources\\EARK\\documentation.pdf"));
    representationFile3.SetRenameTo("data3.pdf");
    representation2.AddFile(representationFile3);

    // 2) build SIP, providing an output directory
    IWriteStrategy writeStrategy = SIPBuilderUtils.GetWriteStrategy(WriteStrategyEnum.ZIP, outputPath);
    string zipSIP = sip.Build(writeStrategy);

    return zipSIP;
  }
}