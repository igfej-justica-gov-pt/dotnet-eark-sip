using IP;
using Mets;

namespace dotnet_eark_sip_examples;

/// <summary>
/// This example aims to show how to create an E-ARK SIP without representations.
/// It focus on metadata, documentation and the different options to add them.
/// </summary>
/// <remarks>
/// The result E-ARK SIP zip folder will be written in the same location this example is run at.
/// </remarks>
internal static class Example4
{
  public static void Run()
  {
    // instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_Example_4", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.1.0");
    sip.AddSubmitterAgent("dotnet-eark-sip-tests");

    // set optional human-readable description
    sip.SetDescription("An example E-ARK SIP without representations");

    // add descriptive metadata (SIP level)
    string metadataFilePath = "Resources\\metadata.xml";
    IPDescriptiveMetadata descriptiveMetadata = new(
      new IPFile(metadataFilePath),
      new MetadataType(IMetadataMdtype.DC),
      null
    );
    sip.AddDescriptiveMetadata(descriptiveMetadata);

    // add preservation metadata (SIP level)
    string preservationFilePath = "Resources\\metadata_preservation.xml";
    IPMetadata metadataPreservation = new(new IPFile(preservationFilePath));
    metadataPreservation.SetMetadataType(IMetadataMdtype.PREMIS);
    sip.AddPreservationMetadata(metadataPreservation);

    // add other metadata (SIP level)
    string otherMetadataFilePath = "Resources\\metadata_other.txt";
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
    string schemaPath = "Resources\\schema.xsd";
    sip.AddSchema(new IPFile(schemaPath));

    // add documentation (SIP level)
    string documentationPath = "Resources\\documentation.txt";
    sip.AddDocumentation(new IPFile(documentationPath));

    // set optional RODA related information about ancestors
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