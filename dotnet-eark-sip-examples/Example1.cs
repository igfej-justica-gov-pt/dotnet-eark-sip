using IP;

namespace dotnet_eark_sip_examples;

/// <summary>
/// This example aims to show how to create an E-ARK SIP with multiple objects in a representation, using just a path to a folder containing the objects.
/// In this example, one will use <c>RepresentationUtils</c>, in order to add all files under a folder to a representation.
/// </summary>
/// <remarks>
/// The result E-ARK SIP zip folder will be written in the same location this example is run at.
/// </remarks>
internal static class Example1 {
  public static void Run() {
    // 1) instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_Example_1", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.2.0");
    sip.AddSubmitterAgent("dotnet-eark-sip-examples", "1");

    // 1.1) set optional human-readable description
    sip.SetDescription("An example E-ARK SIP getting all representation files from a folder");

    // add documentation (SIP level)
    IIPFile documentationFile = new IPFile("Resources\\documentation.txt");
    sip.AddDocumentation(documentationFile);

    // add descriptive metadata (SIP level)
    IIPFile metadataFile = new IPFile("Resources\\metadata.xml");
    IPDescriptiveMetadata descriptiveMetadata = new(metadataFile, new MetadataType(Mets.IMetadataMdtype.EAD), null);
    sip.AddDescriptiveMetadata(descriptiveMetadata);

    // add a representation (status will be set to the default value, i.e. ORIGINAL)
    IPRepresentation representation = new IPRepresentation("representation_1");
    sip.AddRepresentation(representation);

    // add all files inside a folder to the representation
    string path = "Resources\\Representation";
    RepresentationUtils.IncludeInRepresentation(path, representation);

    // build SIP, providing an output directory
    ZipWriteStrategyFactory zipWriteStrategyFactory = new();
    IWriteStrategy writeStrategy = zipWriteStrategyFactory.Create(Directory.GetCurrentDirectory());
    string zipSIP = sip.Build(writeStrategy);

    Console.WriteLine("SIP created at: {0}", zipSIP);
  }
}