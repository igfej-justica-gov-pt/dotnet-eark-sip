using Mono.Options;

public class Metadata {
  public string MetadataFile { get; private set; }
  public string MetadataType { get; private set; }
  public string MetadataVersion { get; private set; }
  public string MetadataSchema { get; private set; }

  public Metadata() {}

  public Metadata(string metadataFile, string metadataType, string metadataVersion = null, string metadataSchema = null) {
    MetadataFile = metadataFile;
    MetadataType = metadataType;
    MetadataVersion = metadataVersion;
    MetadataSchema = metadataSchema;
  }

  public void ParseArguments(string[] args) {
    bool showHelp = false;

    OptionSet options = new() {
      { "metadata-file", "Path to descriptive metadata file", f => MetadataFile = f },
      { "metadata-type=", "Descriptive metadata type", t => MetadataType = t },
      { "metadata-version=", "Descriptive metadata version", v => MetadataVersion = v },
      { "metadata-schema=", "Path to descriptive metadata schema file", s => MetadataSchema = s },
      { "h|help", "Show help message", h => showHelp = h != null }
    };

    try {
      options.Parse(args);
    } catch (OptionException e) {
      Console.WriteLine($"Error: {e.Message}");
      Console.WriteLine("Use --help for usage information.");
      return;
    }

    if (showHelp) {
      Console.WriteLine("Usage: app --metadata-file <filePath> --metadata-type <type> --metadata-version <version> --metadata-schema <schemaPath>");
      options.WriteOptionDescriptions(Console.Out);
      return;
    }
  }

  public void PrintDetails() {
    Console.WriteLine($"Metadata File: {MetadataFile}");
    Console.WriteLine($"Metadata Type: {MetadataType}");
    if (MetadataVersion != null) Console.WriteLine($"Metadata Version: {MetadataVersion}");
    if (MetadataSchema != null) Console.WriteLine($"Metadata Schema: {MetadataSchema}");
  }
}