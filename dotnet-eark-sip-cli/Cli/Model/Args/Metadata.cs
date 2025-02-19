using CommandLine;

public class Metadata {
  [Option("metadata-file", Required = true, HelpText = "Path to descriptive metadata file")]
  public string MetadataFile { get; set; }
  [Option("metadata-type", Required = true, HelpText = "Descriptive metadata type")]
  public string MetadataType { get; set; }
  [Option("metadata-version", Required = false, HelpText = "Descriptive metadata version")]
  public string? MetadataVersion { get; set; }
  [Option("metadata-schema", Required = false, HelpText = "Path to descriptive metadata schema file")]
  public string? MetadataSchema { get; set; }

  public Metadata() {
    MetadataFile = string.Empty;
    MetadataType = string.Empty;
  }

  public Metadata(string metadataFile, string metadataType, string? metadataVersion = null, string? metadataSchema = null) {
    MetadataFile = metadataFile;
    MetadataType = metadataType;
    MetadataVersion = metadataVersion;
    MetadataSchema = metadataSchema;
  }

  public void PrintDetails() {
    Console.WriteLine($"Metadata File: {MetadataFile}");
    Console.WriteLine($"Metadata Type: {MetadataType}");
    if (MetadataVersion != null) Console.WriteLine($"Metadata Version: {MetadataVersion}");
    if (MetadataSchema != null) Console.WriteLine($"Metadata Schema: {MetadataSchema}");
  }
}