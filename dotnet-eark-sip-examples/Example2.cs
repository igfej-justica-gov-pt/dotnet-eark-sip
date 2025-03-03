using IP;

namespace dotnet_eark_sip_examples;

/// <summary>
/// This example aims to show how to create an E-ARK SIP with objects in a representation.
/// A representation file is created and added to the representation.
/// Another representation file is created and added to the same representation, inside a folder and subfolder.
/// Multiple representations can be created similarly.
/// </summary>
/// <remarks>
/// The result E-ARK SIP zip folder will be written in the same location this example is run at.
/// </remarks>
internal static class Example2 {
  public static void Run() {
    // instantiate E-ARK SIP object
    SIP sip = new EARKSIP("SIP_Example_2", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.2.0");
    sip.AddSubmitterAgent("dotnet-eark-sip-examples", "2");

    // set optional human-readable description
    sip.SetDescription("An example E-ARK SIP only with one representation");

    // add a representation (status will be set to the default value, i.e. ORIGINAL)
    IPRepresentation representation = new IPRepresentation("representation_2");
    sip.AddRepresentation(representation);

    // add a file to the representation
    string filePath = "Resources\\Representation\\example.txt";
    IPFile representationFile = new IPFile(filePath);
    representation.AddFile(representationFile);

    // add a file to the representation and put it inside a folder
    // called 'abc' which has a folder inside called 'def'
    IPFile representationFile2 = new(filePath);
    representationFile2.SetRelativeFolders(["abc", "def"]);
    representation.AddFile(representationFile2);

    // build SIP, providing an output directory
    ZipWriteStrategyFactory zipWriteStrategyFactory = new();
    IWriteStrategy writeStrategy = zipWriteStrategyFactory.Create(Directory.GetCurrentDirectory());
    string zipSIP = sip.Build(writeStrategy);

    Console.WriteLine("SIP created at: {0}", zipSIP);
  }
}