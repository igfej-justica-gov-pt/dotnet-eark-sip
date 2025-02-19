public class CSIPVersion {
  public static readonly CSIPVersion V204 = new("2.0.4");
  public static readonly CSIPVersion V210 = new("2.1.0");
  public static readonly CSIPVersion V220 = new("2.2.0");


  private string Version { get; }

  private CSIPVersion(string version) {
    Version = version;
  }

  public override string ToString() => Version;
}