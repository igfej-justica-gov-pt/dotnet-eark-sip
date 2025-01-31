using Mets;

public class FileZipEntryInfo : IZipEntryInfo {
  public string Name { get; private set; }
  public string FilePath { get; private set; }
  public IFilecoreChecksumtype Checksum { get; set; }
  public string ChecksumAlgorithm { get; set; }

  public FileZipEntryInfo(string name, string filePath) {
    Name = name;
    FilePath = filePath;
  }

  public virtual void PrepareEntryForZipping() {}
}