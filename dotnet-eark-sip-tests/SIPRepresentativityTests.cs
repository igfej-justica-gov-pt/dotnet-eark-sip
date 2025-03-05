using IP;
using Mets;
using System.IO.Compression;
using System.Xml.Serialization;
using Xml.Mets.CsipExtensionMets;

namespace dotnet_eark_sip_tests;

[Collection("Non-Parallel Tests")]
public class SIPRepresentativityTests : IDisposable {
  private readonly string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "representativity_tests");

  private readonly string resourcesPath = Path.Combine("Resources", "EARK");

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

    using (ZipArchive archive = ZipFile.OpenRead(zipPath)) {
      // Root METS file exists
      ZipArchiveEntry? mets = archive.GetEntry("SIP_1/METS.xml");
      Assert.NotNull(mets);

      // Representation METS file exists
      ZipArchiveEntry? representationMets = archive.GetEntry("SIP_1/representations/representation 1/METS.xml");
      Assert.NotNull(representationMets);

      // Representation files are present and in the right path
      Assert.NotNull(archive.GetEntry("SIP_1/representations/representation 1/data/documentation.pdf"));
      Assert.NotNull(archive.GetEntry("SIP_1/representations/representation 1/data/test/data.txt"));

      if (representationMets != null) {
        string destinationPath = Path.Combine(outputPath, "extracted-mets.xml");
        representationMets.ExtractToFile(Path.Combine(outputPath, "extracted-mets.xml"));

        XmlSerializer serializer = new(typeof(Mets.Mets));
        Mets.Mets? metsObject;

        string xmlContent = File.ReadAllText(destinationPath);
        using (StringReader reader = new(xmlContent)) {
          object? obj = serializer.Deserialize(reader);
          if (obj != null) {
            metsObject = (Mets.Mets)obj;
          } else {
            metsObject = null;
          }
        }

        Assert.NotNull(metsObject);
        Assert.Equal(Oaispackagetype.SIP, metsObject.MetsHdr.Oaispackagetype);
        Assert.NotEmpty(metsObject.AmdSec);
        Assert.Single(metsObject.FileSec.FileGrp);
        Assert.Equal("Data", metsObject.FileSec.FileGrp[0].Use);
        Assert.Equal(2, metsObject.FileSec.FileGrp[0].File.Count);
        Assert.Single(metsObject.StructMap);
        Assert.Equal("PHYSICAL", metsObject.StructMap[0].Type);
        Assert.Equal("CSIP", metsObject.StructMap[0].Label);
        Assert.Equal("representation 1", metsObject.StructMap[0].Div.Label);
        Assert.Equal("ORIGINAL", metsObject.StructMap[0].Div.Type);
      }
    }
  }

  /// <summary>
  /// Creates a full EARKSIP to be tested.
  /// </summary>
  /// <returns>
  /// Path to the generated EARKSIP zip.
  /// </returns>
  private string CreateFullEARKSIPWithFolders() {
    string currentPath = Directory.GetCurrentDirectory();

    // 1) instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.2.0");
    sip.AddSubmitterAgent("SIP Representativity Tests");

    // 1.1) set optional human-readable description
    sip.SetDescription("A full E-ARK SIP with folders");

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

    string folderPath = Path.Combine(currentPath, "Resources","Representative");
    RepresentationUtils.IncludeInRepresentation(folderPath, representation1);

    // 2) build SIP, providing an output directory
    IWriteStrategy writeStrategy = SIPBuilderUtils.GetWriteStrategy(WriteStrategyEnum.ZIP, outputPath);
    string zipSIP = sip.Build(writeStrategy);

    return zipSIP;
  }
}