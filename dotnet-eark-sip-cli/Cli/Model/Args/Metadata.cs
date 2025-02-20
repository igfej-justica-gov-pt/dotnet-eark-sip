public class Metadata {
  public string MetadataFile { get; set; }
  public string MetadataType { get; set; }
  public string? MetadataVersion { get; set; }
  public string? MetadataSchema { get; set; }

  public Metadata() {
    MetadataFile = "";
    MetadataType = "";
  }

  public Metadata(string metadataFile, string metadataType, string? metadataVersion = null, string? metadataSchema = null) {
    MetadataFile = metadataFile;
    MetadataType = metadataType;
    MetadataVersion = string.IsNullOrEmpty(metadataVersion) ? null : metadataVersion;
    MetadataSchema = string.IsNullOrEmpty(metadataSchema) ? null : metadataSchema;
  }

  public void PrintDetails() {
    Console.WriteLine($"Metadata File: {MetadataFile}");
    Console.WriteLine($"Metadata Type: {MetadataType}");
    if (MetadataVersion != null) Console.WriteLine($"Metadata Version: {MetadataVersion}");
    if (MetadataSchema != null) Console.WriteLine($"Metadata Schema: {MetadataSchema}");
  }
}