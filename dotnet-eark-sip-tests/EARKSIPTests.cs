using IP;
using Mets;

namespace dotnet_eark_sip_tests;

[Collection("Non-Parallel Tests")]
public class EARKSIPTests : IDisposable {
  private readonly string REPRESENTATION_STATUS_NORMALIZED = "NORMALIZED";
  private readonly string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "earksip_tests");

  /// <summary>
	/// Test initialization: create output directory
	/// </summary>
	public EARKSIPTests() {
		Directory.CreateDirectory(outputPath);
	}

  /// <summary>
	/// Tests cleanup: delete temp files and directory
	/// </summary>
  public void Dispose() {
    Utils.DeleteDirectory(outputPath);
  }

  /// <summary>
	/// Test creation of EARKSIP.
	/// </summary>
  [Fact]
  public void Create_FullEARKSIP_CreatesZipWithCorrectFiles() {
    string zipPath = CreateFullEARKSIPForTestCompliance();
    Assert.True(File.Exists(zipPath));

    // TODO: Add additional verifications
  }

  /// <summary>
	/// Test creation of EARKSIP-Shallow.
	/// </summary>
  [Fact]
  public void Create_FullEARKSIPShallow_CreatesZipWithCorrectFiles() {
    string zipPath = CreateFullEARKSIPShallowForTestCompliance();
    Assert.True(File.Exists(zipPath));

    // TODO: Add additional verifications
  }

  /// <summary>
	/// Auxiliary method to create a full EARK SIP for testing purposes.
	/// </summary>
  /// <returns>
  /// Path to the generated EARKSIP zip.
  /// </returns>
  private string CreateFullEARKSIPForTestCompliance() {
    string currentPath = Directory.GetCurrentDirectory();

    // 1) instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.1.0");
    sip.AddSubmitterAgent("dotnet-eark-sip-tests");

    // 1.1) set optional human-readable description
    sip.SetDescription("A full E-ARK SIP");

    // 1.2) add descriptive metadata (SIP level)
    string metadataFilePath = Path.Combine(currentPath, ".\\Resources\\EARK\\metadata_descriptive_dc.xml");
    IPDescriptiveMetadata descriptiveMetadata = new(
      new IPFile(metadataFilePath),
      new MetadataType(IMetadataMdtype.DC),
      null
    );
    sip.AddDescriptiveMetadata(descriptiveMetadata);

    // 1.3) add preservation metadata (SIP level)
    string preservationFilePath = Path.Combine(currentPath, ".\\Resources\\EARK\\metadata_preservation_premis.xml");
    IPMetadata metadataPreservation = new(new IPFile(preservationFilePath));
    metadataPreservation.SetMetadataType(IMetadataMdtype.PREMIS);
    sip.AddPreservationMetadata(metadataPreservation);

    // 1.4) add other metadata (SIP level)
    string otherMetadataFilePath = Path.Combine(currentPath, ".\\Resources\\EARK\\metadata_other.txt");
    IPFile metadataOtherFile = new(otherMetadataFilePath);
    // 1.4.1) optionally one may rename file final name
    metadataOtherFile.SetRenameTo("metadata_other_renamed.txt");
    IPMetadata metadataOther = new(metadataOtherFile);
    sip.AddOtherMetadata(metadataOther);
    // 1.4.1) optionally one may rename file final name
    metadataOtherFile = new IPFile(otherMetadataFilePath);
    metadataOtherFile.SetRenameTo("metadata_other_renamed2.txt");
    metadataOther = new IPMetadata(metadataOtherFile);
    sip.AddOtherMetadata(metadataOther);

    // 1.5) add xml schema (SIP level)
    string schemaPath = Path.Combine(currentPath, ".\\Resources\\EARK\\schema.xsd");
    sip.AddSchema(new IPFile(schemaPath));

    // 1.6) add documentation (SIP level)
    string documentationPath = Path.Combine(currentPath, ".\\Resources\\EARK\\documentation.pdf");
    sip.AddDocumentation(new IPFile(documentationPath));

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

    // 1.9.1) Add a file to the representation
    string filePath = Path.Combine(currentPath, ".\\Resources\\EARK\\documentation.pdf");
    IPFile representationFile = new(filePath);
    representationFile.SetRenameTo("data_.pdf");
    representation1.AddFile(representationFile);

    // SIDE TEST: encoding
    if (!Utils.IsWindowsSystem()) {
      IPFile representationFileEnc1 = new IPFile(filePath);
      representationFileEnc1.SetRenameTo("enc1_\u0001\u001F.pdf");
      representation1.AddFile(representationFileEnc1);
    }

    IPFile representationFileEnc2 = new IPFile(filePath);
    representationFileEnc2.SetRenameTo("enc2_\u0080\u0081\u0090\u00FF.pdf");
    representation1.AddFile(representationFileEnc2);

    IPFile representationFileEnc3 = new IPFile(filePath);
    representationFileEnc3.SetRenameTo(Utils.IsWindowsSystem() ? "enc3_;@=&.pdf" : "enc3_;?:@=&.pdf");
    representation1.AddFile(representationFileEnc3);

    IPFile representationFileEnc4 = new IPFile(filePath);
    representationFileEnc4
      .SetRenameTo(Utils.IsWindowsSystem() ? "enc4_#%{}\\^~[ ]`.pdf" : "enc4_\"<>#%{}|\\^~[ ]`.pdf");
    representation1.AddFile(representationFileEnc4);

    IPFile representationFileEnc5 = new IPFile(filePath);
    representationFileEnc5
      .SetRenameTo(Utils.IsWindowsSystem() ? "enc4_#+%{}\\^~[ ]`.pdf" : "enc4_\"<>+#%{}|\\^~[ ]`.pdf");
    representation1.AddFile(representationFileEnc5);

    // 1.9.2) add a file to the representation and put it inside a folder
    // called 'abc' which has a folder inside called 'def'
    IPFile representationFile2 = new(filePath);
    representationFile2.SetRelativeFolders(["abc", "def"]);
    representation1.AddFile(representationFile2);

    // 1.10) add a representation & define its status
    IPRepresentation representation2 = new("representation 2");
    representation2.SetStatus(new RepresentationStatus(REPRESENTATION_STATUS_NORMALIZED));
    sip.AddRepresentation(representation2);

    // 1.10.1) add a file to the representation
    IPFile representationFile3 = new(filePath);
    representationFile3.SetRenameTo("data3.pdf");
    representation2.AddFile(representationFile3);

    // 2) build SIP, providing an output directory
    ZipWriteStrategyFactory zipWriteStrategyFactory = new();
    IWriteStrategy writeStrategy = zipWriteStrategyFactory.Create(outputPath);
    string zipSIP = sip.Build(writeStrategy);

    return zipSIP;
  }

  /// <summary>
	/// Auxiliary method to create a full EARKSIP-Shallow for testing purposes.
	/// </summary>
  /// <returns>
  /// Path to the generated EARKSIP-Shallow zip.
  /// </returns>
  private string CreateFullEARKSIPShallowForTestCompliance() {
    string currentPath = Directory.GetCurrentDirectory();

    // 1) instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_S_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.1.0");
    sip.AddSubmitterAgent("dotnet-eark-sip-tests");

    // 1.1) set optional human-readable description
    sip.SetDescription("A full E-ARK SIP-Shallow");

    // 1.2) add descriptive metadata (SIP level)
    string metadataFilePath = Path.Combine(currentPath, ".\\Resources\\EARK\\metadata_descriptive_dc.xml");
    IPDescriptiveMetadata descriptiveMetadata = new(
      new IPFile(metadataFilePath),
      new MetadataType(IMetadataMdtype.DC),
      null
    );
    sip.AddDescriptiveMetadata(descriptiveMetadata);

    // 1.3) add preservation metadata (SIP level)
    string preservationFilePath = Path.Combine(currentPath, ".\\Resources\\EARK\\metadata_preservation_premis.xml");
    IPMetadata metadataPreservation = new(new IPFile(preservationFilePath));
    metadataPreservation.SetMetadataType(IMetadataMdtype.PREMIS);
    sip.AddPreservationMetadata(metadataPreservation);

    // 1.4) add other metadata (SIP level)
    string otherMetadataFilePath = Path.Combine(currentPath, ".\\Resources\\EARK\\metadata_other.txt");
    IPFile metadataOtherFile = new(otherMetadataFilePath);
    // 1.4.1) optionally one may rename file final name
    metadataOtherFile.SetRenameTo("metadata_other_renamed.txt");
    IPMetadata metadataOther = new(metadataOtherFile);
    sip.AddOtherMetadata(metadataOther);
    // 1.4.1) optionally one may rename file final name
    metadataOtherFile = new IPFile(otherMetadataFilePath);
    metadataOtherFile.SetRenameTo("metadata_other_renamed2.txt");
    metadataOther = new IPMetadata(metadataOtherFile);
    sip.AddOtherMetadata(metadataOther);

    // 1.5) add xml schema (SIP level)
    string schemaPath = Path.Combine(currentPath, ".\\Resources\\EARK\\schema.xsd");
    sip.AddSchema(new IPFile(schemaPath));

    // 1.6) add documentation (SIP level)
    string documentationPath = Path.Combine(currentPath, ".\\Resources\\EARK\\documentation.pdf");
    sip.AddDocumentation(new IPFile(documentationPath));

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

    // 1.9.1) add a file to the representation
    Uri url = new Uri(Path.Combine(currentPath, ".\\Resources\\EARK\\data.txt"));
    Uri url2 = new Uri(Path.Combine(currentPath, ".\\Resources\\EARK\\descriptive.txt"));
    Uri url3 = new Uri(Path.Combine(currentPath, ".\\Resources\\EARK\\eark.pdf"));
    // setting basic information about the remote file
    FileType filetype = new FileType
    {
      Mimetype = "application/pdf",
      Size = 784099L,
      SizeSpecified = true,
      Created = DateTime.Now,
      CreatedSpecified = true,
      Checksum = "3df79d34abbca99308e79cb94461c1893582604d68329a41fd4bec1885e6adb4",
      Checksumtype = IFilecoreChecksumtype.SHA256,
      ChecksumtypeSpecified = true
    };

    FileType filetype2 = new FileType
    {
      Mimetype = "application/pdf",
      Size = 784099L,
      SizeSpecified = true,
      Created = DateTime.Now,
      CreatedSpecified = true,
      Checksum = "3df79d34abbca99308e79cb94461c1893582604d68329a41fd4bec1885e6adb4",
      Checksumtype = IFilecoreChecksumtype.SHA256,
      ChecksumtypeSpecified = true
    };

    FileType filetype3 = new FileType
    {
      Mimetype = "application/pdf",
      Size = 784099L,
      SizeSpecified = true,
      Created = DateTime.Now,
      CreatedSpecified = true,
      Checksum = "3df79d34abbca99308e79cb94461c1893582604d68329a41fd4bec1885e6adb4",
      Checksumtype = IFilecoreChecksumtype.SHA256,
      ChecksumtypeSpecified = true
    };

    IIPFile representationFile = new IPFileShallow(url, filetype);
    List<string> relativeFolders = new List<string> { "abc", "def" };
    ((IPFileShallow) representationFile).SetRelativeFolders(relativeFolders);

    // New fil

    IIPFile representationFile2 = new IPFileShallow(url2, filetype2);
    List<string> relativeFolders2 = new List<string> { "abc", "fgh" };
    ((IPFileShallow) representationFile2).SetRelativeFolders(relativeFolders2);

    IIPFile representationFile3 = new IPFileShallow(url3, filetype3);
    List<string> relativeFolders3 = new List<string>();
    ((IPFileShallow) representationFile3).SetRelativeFolders(relativeFolders3);

    List<string> relativeFolders4 = new List<string> { "uuu", "fff" };
    IIPFile representationFile4 = new IPFileShallow(relativeFolders4);

    representation1.AddFile(representationFile);
    representation1.AddFile(representationFile2);
    representation1.AddFile(representationFile3);
    representation1.AddFile(representationFile4);

    // 2) build SIP, providing an output directory
    ZipWriteStrategyFactory zipWriteStrategyFactory = new();
    IWriteStrategy writeStrategy = zipWriteStrategyFactory.Create(outputPath);
    string zipSIP = sip.Build(writeStrategy, "okok", IPEnums.SIPType.EARK2S);

    return zipSIP;
  }
}