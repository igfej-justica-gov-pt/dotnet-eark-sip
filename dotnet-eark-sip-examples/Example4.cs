using IP;
using Mets;

namespace dotnet_eark_sip_examples;

/// <summary>
/// This example aims to show how to create an E-ARK SIP without representations.
/// It focus on multiple options available while creating a SIP, 
/// like adding metadata, setting ancestors and adding different agents.
/// </summary>
/// <remarks>
/// The result E-ARK SIP zip folder will be written in the same location this example is run at.
/// </remarks>
internal static class Example4 {
  private readonly static string separator = Path.DirectorySeparatorChar.ToString();
  private readonly static string resourcesPath = "Resources" + separator;

  public static void Run() {
    // instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_Example_4", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.2.0");
    sip.AddSubmitterAgent("dotnet-eark-sip-tests");

    // set optional human-readable description
    sip.SetDescription("An example E-ARK SIP without representations");

    // add descriptive metadata (SIP level)
    string metadataFilePath = resourcesPath + "metadata.xml";
    IPDescriptiveMetadata descriptiveMetadata = new(
      new IPFile(metadataFilePath),
      new MetadataType(IMetadataMdtype.DC),
      null
    );
    sip.AddDescriptiveMetadata(descriptiveMetadata);

    // add preservation metadata (SIP level)
    string preservationFilePath = resourcesPath + "metadata_preservation.xml";
    IPMetadata metadataPreservation = new(new IPFile(preservationFilePath));
    metadataPreservation.SetMetadataType(IMetadataMdtype.PREMIS);
    sip.AddPreservationMetadata(metadataPreservation);

    // add other metadata (SIP level)
    string otherMetadataFilePath = resourcesPath + "metadata_other.txt";
    IPFile metadataOtherFile = new(otherMetadataFilePath);
    // optionally one may rename file final name
    metadataOtherFile.SetRenameTo("metadata_other_renamed.txt");
    IPMetadata metadataOther = new(metadataOtherFile);
    sip.AddOtherMetadata(metadataOther);
    // optionally one may rename file final name
    metadataOtherFile = new IPFile(otherMetadataFilePath);
    metadataOtherFile.SetRenameTo("metadata_other_renamed2.txt");
    metadataOther = new IPMetadata(metadataOtherFile);
    sip.AddOtherMetadata(metadataOther);

    // add xml schema (SIP level)
    string schemaPath = resourcesPath + "schema.xsd";
    sip.AddSchema(new IPFile(schemaPath));

    // add documentation (SIP level)
    string documentationPath = resourcesPath + "documentation.txt";
    sip.AddDocumentation(new IPFile(documentationPath));

    // set optional related information about ancestors
    sip.SetAncestors(["b6f24059-8973-4582-932d-eb0b2cb48f28"]);

    // add an agent (SIP level)
    sip.AddAgent(new IPAgent(
      "Agent Name",
      MetsTypeMetsHdrAgentRole.OTHER,
      "OTHER ROLE",
      MetsTypeMetsHdrAgentType.INDIVIDUAL,
      "OTHER TYPE",
      "",
      Xml.Mets.CsipExtensionMets.Notetype.SOFTWARE_VERSION
    ));

    // build SIP, providing an output directory
    ZipWriteStrategyFactory zipWriteStrategyFactory = new();
    IWriteStrategy writeStrategy = zipWriteStrategyFactory.Create(Directory.GetCurrentDirectory());
    string zipSIP = sip.Build(writeStrategy);

    Console.WriteLine("SIP created at: {0}", zipSIP);
  }
}