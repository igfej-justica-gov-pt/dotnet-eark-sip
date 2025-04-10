using IP;

namespace dotnet_eark_sip_examples;

/// <summary>
/// This example creates multiple E-ARK SIPs, each with one object in a representation.
/// Each representation has a defined status and documentation and metadata files associated.
/// Similarly, preservation metadata, other metadata and schemas can be added to representations.
/// </summary>
/// <remarks>
/// The result E-ARK SIP zip folder will be written in the same location this example is run at.
/// </remarks>
internal static class Example3 {
  private readonly static string resourcesPath = "Resources";

  public static void Run() {
    // Create a destination folder to create the SIPs to
    string destination = Path.Combine(Directory.GetCurrentDirectory(), "SIPs");
    Directory.CreateDirectory(destination);

    // Create the SIPs
    string input = Path.Combine(resourcesPath, "Representation");
    CreateSIPs(input, destination);
    Console.WriteLine("SIPs created at {0}", destination);
  }

  private static void CreateSIPs(string inputPath, string destinationPath, string? prefix = null) {
    // Traverse the files on the folder and generate a SIP for each
    string[] files = Directory.GetFiles(inputPath);
    for (int i = 0; i < files.Length; i++) {
      // Calculate identifier (relative path + index)
      string id = (prefix ?? "") + i.ToString();
      CreateSIP(files[i], destinationPath, id);
    }

    // Traverse all subdirectories and recursively generate a SIP for each file in it
    string[] directories = Directory.GetDirectories(inputPath);
    for (int i = 0; i < directories.Length; i++) {
      // Calculate new prefix - get directory name and add to previous prefix ("" if null)
      string directoryName = directories[i].Split(Path.DirectorySeparatorChar).Last();
      string newPrefix = prefix ?? "" + directoryName + "_";
      // Recursively create SIPs for each subfolder
      CreateSIPs(directories[i], destinationPath, newPrefix);
    }
  }

  private static void CreateSIP(string filePath, string destinationPath, string id) {
    // instantiate E-ARK SIP object
    SIP sip = new EARKSIP($"SIP_Example_3_{id}", IPContentType.GetMIXED(), IPContentInformationType.GetMIXED(), "2.2.0");
    sip.AddSubmitterAgent("dotnet-eark-sip-examples", "3");

    // set optional human-readable description
    sip.SetDescription("An example E-ARK SIP with one representation containing only one data file and metadata associated, generated alongside other E-ARK SIPs");

    // add a representation & define its status
    IPRepresentation representation = new IPRepresentation("representation_3");
    representation.SetStatus(new RepresentationStatus("NORMALIZED"));
    sip.AddRepresentation(representation);

    // add a file to the representation
    IPFile representationFile = new(filePath);
    representation.AddFile(representationFile);

    // add documentation (representation level)
    string documentationFilePath = Path.Combine(resourcesPath, "documentation.txt");
    IIPFile documentationFile = new IPFile(documentationFilePath);
    sip.AddDocumentationToRepresentation(representation.RepresentationID, documentationFile);

    // add descriptive metadata (representation level)
    string metadataFilePath = Path.Combine(resourcesPath, "metadata.xml");
    IIPFile metadataFile = new IPFile(metadataFilePath);
    IPDescriptiveMetadata descriptiveMetadata = new(metadataFile, new MetadataType(Mets.IMetadataMdtype.EAD), null);
    sip.AddDescriptiveMetadataToRepresentation(representation.RepresentationID, descriptiveMetadata);

    // build SIP, providing an output directory
    ZipWriteStrategyFactory zipWriteStrategyFactory = new();
    IWriteStrategy writeStrategy = zipWriteStrategyFactory.Create(destinationPath);
    string zip = sip.Build(writeStrategy);

    Console.WriteLine("SIP \"{0}\" created at: {1}", id, zip);
  }
}