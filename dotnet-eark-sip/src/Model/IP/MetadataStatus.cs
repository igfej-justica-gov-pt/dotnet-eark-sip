public class MetadataStatus {
  public static readonly MetadataStatus CURRENT = new MetadataStatus("CURRENT");
  public static readonly MetadataStatus SUPERSEDED = new MetadataStatus("SUPERSEDED");

  public string Status { get; }

  private MetadataStatus(string status) {
    Status = status;
  }

  public override string ToString() => Status;
}