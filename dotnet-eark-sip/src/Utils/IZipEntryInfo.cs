using Mets;

public interface IZipEntryInfo {
  string Name { get; }
  string FilePath { get; }
  string Checksum { get; set; }
  IFilecoreChecksumtype ChecksumAlgorithm { get; set; }
  void PrepareEntryForZipping();
}