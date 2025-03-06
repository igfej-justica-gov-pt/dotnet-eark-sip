using IP;
using Mets;
using System.IO.Compression;
using System.Xml.Serialization;
using Xml.Mets.CsipExtensionMets;

namespace dotnet_eark_sip_tests;

[Collection("Non-Parallel Tests")]
public class EARKSIPTests : IDisposable {
  private readonly string REPRESENTATION_STATUS_NORMALIZED = "NORMALIZED";
  private readonly string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "earksip_tests");

  private readonly string resourcesPath = "Resources" + Path.DirectorySeparatorChar.ToString() + "EARK";

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

    using (ZipArchive archive = ZipFile.OpenRead(zipPath)) {
      // Root METS file exists
      ZipArchiveEntry? rootMets = archive.GetEntry("SIP_1/METS.xml");
      Assert.NotNull(rootMets);

      // All files are correctly placed
      // Representations' METS
      Assert.NotNull(archive.GetEntry("SIP_1/representations/representation 1/METS.xml"));
      Assert.NotNull(archive.GetEntry("SIP_1/representations/representation 2/METS.xml"));
      // Metadata
      Assert.NotNull(archive.GetEntry("SIP_1/metadata/descriptive/metadata_descriptive_dc.xml"));
      Assert.NotNull(archive.GetEntry("SIP_1/metadata/preservation/metadata_preservation_premis.xml"));
      Assert.NotNull(archive.GetEntry("SIP_1/metadata/other/metadata_other_renamed.txt"));
      Assert.NotNull(archive.GetEntry("SIP_1/metadata/other/metadata_other_renamed2.txt"));
      // Documentation
      Assert.NotNull(archive.GetEntry("SIP_1/documentation/documentation.pdf"));
      // Schemas
      Assert.NotNull(archive.GetEntry("SIP_1/schemas/schema.xsd"));
      Assert.NotNull(archive.GetEntry("SIP_1/schemas/DILCISExtensionMETS.xsd"));
      Assert.NotNull(archive.GetEntry("SIP_1/schemas/DILCISExtensionSIPMETS.xsd"));
      Assert.NotNull(archive.GetEntry("SIP_1/schemas/mets1_12_1.xsd"));
      Assert.NotNull(archive.GetEntry("SIP_1/schemas/xlink.xsd"));

      // Check root METS file
      if (rootMets != null) {
        string destinationPath = Path.Combine(outputPath, "extracted-root-mets.xml");
        rootMets.ExtractToFile(Path.Combine(outputPath, "extracted-root-mets.xml"));

        XmlSerializer serializer = new(typeof(Mets.Mets));
        Mets.Mets? metsObject;

        string xmlContent = File.ReadAllText(destinationPath);
        using (StringReader reader = new(xmlContent)) {
          object? obj = serializer.Deserialize(reader);
          if (obj != null) {
            metsObject = (Mets.Mets)obj;
          }
          else {
            metsObject = null;
          }
        }

        // Root object
        Assert.NotNull(metsObject);
        Assert.Equal("SIP_1", metsObject.Objid);
        Assert.Equal("Mixed", metsObject.Type);
        Assert.Equal(Contentinformationtype.MIXED, metsObject.Contentinformationtype);
        Assert.Equal("https://earksip.dilcis.eu/profile/E-ARK-SIP-v2-2-0.xml", metsObject.Profile);
        // Header
        Assert.Equal("NEW", metsObject.MetsHdr.Recordstatus);
        Assert.Equal(Oaispackagetype.SIP, metsObject.MetsHdr.Oaispackagetype);
        /// Agents
        Assert.Equal(3, metsObject.MetsHdr.Agent.Count);
        List<string> agentNames = metsObject.MetsHdr.Agent.Select(agent => agent.Name).ToList();
        Assert.Contains("Agent Name", agentNames);
        Assert.Contains("dotnet-eark-sip", agentNames);
        Assert.Contains("dotnet-eark-sip-tests", agentNames);
        List<MetsTypeMetsHdrAgentRole> roles = metsObject.MetsHdr.Agent.Select(agent => agent.Role).ToList();
        Assert.Contains(MetsTypeMetsHdrAgentRole.CREATOR, roles);
        Assert.Contains(MetsTypeMetsHdrAgentRole.OTHER, roles);
        List<MetsTypeMetsHdrAgentType> types = metsObject.MetsHdr.Agent.Select(agent => agent.Type).ToList();
        Assert.Contains(MetsTypeMetsHdrAgentType.INDIVIDUAL, types);
        Assert.Contains(MetsTypeMetsHdrAgentType.OTHER, types);
        // Metadata sections
        /// Descriptive metadata
        Assert.Equal(3, metsObject.DmdSec.Count);
        List<IMetadataMdtype> mdTypes = metsObject.DmdSec.Select(dmd => dmd.MdRef.Mdtype).ToList();
        Assert.Equal(3, mdTypes.Count);
        Assert.Contains(IMetadataMdtype.DC, mdTypes);
        Assert.Contains(IMetadataMdtype.OTHER, mdTypes);
        /// Administrative metadata
        Assert.Single(metsObject.AmdSec);
        Assert.Single(metsObject.AmdSec[0].DigiprovMd);
        Assert.Equal(IMetadataMdtype.PREMIS, metsObject.AmdSec[0].DigiprovMd[0].MdRef.Mdtype);
        // Files' section
        Assert.Equal(4, metsObject.FileSec.FileGrp.Count);
        List<string> fileGroupUses = metsObject.FileSec.FileGrp.Select(grp => grp.Use).ToList();
        Assert.Contains("Schemas", fileGroupUses);
        Assert.Contains("Documentation", fileGroupUses);
        Assert.Contains("Representations/representation 1", fileGroupUses);
        Assert.Contains("Representations/representation 2", fileGroupUses);
        // Struct maps
        /// CSIP
        Assert.Equal(2, metsObject.StructMap.Count);
        Assert.Equal("PHYSICAL", metsObject.StructMap[0].Type);
        Assert.Equal("CSIP", metsObject.StructMap[0].Label);
        Assert.Equal("SIP_1", metsObject.StructMap[0].Div.Label);
        Assert.Equal(6, metsObject.StructMap[0].Div.Div.Count);
        List<string> labels = metsObject.StructMap[0].Div.Div.Select(div => div.Label).ToList();
        Assert.Contains("Metadata", labels);
        Assert.Contains("Metadata/Other", labels);
        Assert.Contains("Schemas", labels);
        Assert.Contains("Documentation", labels);
        Assert.Contains("Representations/representation 1", labels);
        Assert.Contains("Representations/representation 2", labels);
        /// dotnet-eark-sip structural map
        Assert.Equal("dotnet-eark-sip structural map", metsObject.StructMap[1].Label);
        Assert.Single(metsObject.StructMap[1].Div.Div);
        Assert.Equal("dotnet-eark-sip", metsObject.StructMap[1].Div.Label);
        Assert.Equal("Ancestors", metsObject.StructMap[1].Div.Div[0].Label);
      }
    }
  }

  /// <summary>
	/// Test creation of EARKSIP-Shallow.
	/// </summary>
  [Fact]
  public void Create_FullEARKSIPShallow_CreatesZipWithCorrectFiles() {
    string zipPath = CreateFullEARKSIPShallowForTestCompliance();
    Assert.True(File.Exists(zipPath));
    Assert.Contains("okok", zipPath);

    using (ZipArchive archive = ZipFile.OpenRead(zipPath)) {
      // Root METS file exists
      Assert.NotNull(archive.GetEntry("SIP_S_1/METS.xml"));

      // Representation METS exists
      ZipArchiveEntry? representationMets = archive.GetEntry("SIP_S_1/representations/representation 1/METS.xml");
      Assert.NotNull(representationMets);

      if (representationMets != null) {
        string destinationPath = Path.Combine(outputPath, "extracted-representation-mets.xml");
        representationMets.ExtractToFile(Path.Combine(outputPath, "extracted-representation-mets.xml"));

        XmlSerializer serializer = new(typeof(Mets.Mets));
        Mets.Mets? metsObject;

        string xmlContent = File.ReadAllText(destinationPath);
        using (StringReader reader = new(xmlContent)) {
          object? obj = serializer.Deserialize(reader);
          if (obj != null) {
            metsObject = (Mets.Mets)obj;
          }
          else {
            metsObject = null;
          }
        }

        Assert.NotNull(metsObject);

        // Files
        Assert.Equal(3, metsObject.FileSec.FileGrp.Count);
        List<string> fileGroupUses = metsObject.FileSec.FileGrp.Select(grp => grp.Use).ToList();
        Assert.Contains("Data", fileGroupUses);
        Assert.Contains("data/abc/def/", fileGroupUses);
        Assert.Contains("data/abc/fgh/", fileGroupUses);
        // Struct map
        Assert.Single(metsObject.StructMap);
        Assert.Equal("representation 1", metsObject.StructMap[0].Div.Label);
        Assert.Equal("ORIGINAL", metsObject.StructMap[0].Div.Type);
        Assert.Equal("Data", metsObject.StructMap[0].Div.Div[0].Label);
        Assert.Single(metsObject.StructMap[0].Div.Div[0].Fptr);
        Assert.Equal(2, metsObject.StructMap[0].Div.Div[0].Div.Count);
        Assert.Equal("abc", metsObject.StructMap[0].Div.Div[0].Div[0].Label);
        Assert.Equal(2, metsObject.StructMap[0].Div.Div[0].Div[0].Div.Count);
        Assert.Equal("def", metsObject.StructMap[0].Div.Div[0].Div[0].Div[0].Label);
        Assert.Equal("fgh", metsObject.StructMap[0].Div.Div[0].Div[0].Div[1].Label);
        Assert.Equal("uuu", metsObject.StructMap[0].Div.Div[0].Div[1].Label);
        Assert.Single(metsObject.StructMap[0].Div.Div[0].Div[1].Div);
        Assert.Equal("fff", metsObject.StructMap[0].Div.Div[0].Div[1].Div[0].Label);
      }
    }
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
    SIP sip = new EARKSIP("SIP_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.2.0");
    sip.AddSubmitterAgent("dotnet-eark-sip-tests");

    // 1.1) set optional human-readable description
    sip.SetDescription("A full E-ARK SIP");

    // 1.2) add descriptive metadata (SIP level)
    string metadataFilePath = Path.Combine(currentPath, resourcesPath, "metadata_descriptive_dc.xml");
    IPDescriptiveMetadata descriptiveMetadata = new(
      new IPFile(metadataFilePath),
      new MetadataType(IMetadataMdtype.DC),
      null
    );
    sip.AddDescriptiveMetadata(descriptiveMetadata);

    // 1.3) add preservation metadata (SIP level)
    string preservationFilePath = Path.Combine(currentPath, resourcesPath, "metadata_preservation_premis.xml");
    IPMetadata metadataPreservation = new(new IPFile(preservationFilePath));
    metadataPreservation.SetMetadataType(IMetadataMdtype.PREMIS);
    sip.AddPreservationMetadata(metadataPreservation);

    // 1.4) add other metadata (SIP level)
    string otherMetadataFilePath = Path.Combine(currentPath, resourcesPath, "metadata_other.txt");
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
    string schemaPath = Path.Combine(currentPath, resourcesPath, "schema.xsd");
    sip.AddSchema(new IPFile(schemaPath));

    // 1.6) add documentation (SIP level)
    string documentationPath = Path.Combine(currentPath, resourcesPath, "documentation.pdf");
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
      Notetype.SOFTWARE_VERSION
    ));

    // 1.9) Add a representation (status will be set to the default value, i.e. ORIGINAL)
    IPRepresentation representation1 = new("representation 1");
    sip.AddRepresentation(representation1);

    // 1.9.1) Add a file to the representation
    string filePath = Path.Combine(currentPath, resourcesPath, "documentation.pdf");
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

    // instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_S_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.1.0");
    sip.AddSubmitterAgent("dotnet-eark-sip-tests");

    // set optional human-readable description
    sip.SetDescription("A full E-ARK SIP-Shallow");

    // Add a representation (status will be set to the default value, i.e. ORIGINAL)
    IPRepresentation representation1 = new("representation 1");
    sip.AddRepresentation(representation1);

    // add a file to the representation
    Uri url = new Uri(Path.Combine(currentPath, resourcesPath, "data.txt"));
    Uri url2 = new Uri(Path.Combine(currentPath, resourcesPath, "descriptive.txt"));
    Uri url3 = new Uri(Path.Combine(currentPath, resourcesPath, "eark.pdf"));

    DateTime created = DateTime.Now;

    // setting basic information about the remote file
    FileType filetype = new FileType {
      Mimetype = "application/pdf",
      Size = 784099L,
      SizeSpecified = true,
      Created = created,
      CreatedSpecified = true,
      Checksum = "3df79d34abbca99308e79cb94461c1893582604d68329a41fd4bec1885e6adb4",
      Checksumtype = IFilecoreChecksumtype.SHA256,
      ChecksumtypeSpecified = true
    };

    FileType filetype2 = new FileType {
      Mimetype = "application/pdf",
      Size = 784099L,
      SizeSpecified = true,
      Created = created,
      CreatedSpecified = true,
      Checksum = "3df79d34abbca99308e79cb94461c1893582604d68329a41fd4bec1885e6adb4",
      Checksumtype = IFilecoreChecksumtype.SHA256,
      ChecksumtypeSpecified = true
    };

    FileType filetype3 = new FileType {
      Mimetype = "application/pdf",
      Size = 784099L,
      SizeSpecified = true,
      Created = created,
      CreatedSpecified = true,
      Checksum = "3df79d34abbca99308e79cb94461c1893582604d68329a41fd4bec1885e6adb4",
      Checksumtype = IFilecoreChecksumtype.SHA256,
      ChecksumtypeSpecified = true
    };

    IIPFile representationFile = new IPFileShallow(url, filetype);
    List<string> relativeFolders = new List<string> { "abc", "def" };
    ((IPFileShallow)representationFile).SetRelativeFolders(relativeFolders);

    // New file
    IIPFile representationFile2 = new IPFileShallow(url2, filetype2);
    List<string> relativeFolders2 = new List<string> { "abc", "fgh" };
    ((IPFileShallow)representationFile2).SetRelativeFolders(relativeFolders2);

    IIPFile representationFile3 = new IPFileShallow(url3, filetype3);
    List<string> relativeFolders3 = new List<string>();
    ((IPFileShallow)representationFile3).SetRelativeFolders(relativeFolders3);

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