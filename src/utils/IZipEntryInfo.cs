public interface IZipEntryInfo {
  string GetName();
  string GetFilePath();
  void PrepareEntryForZipping();
  string GetChecksum();
  void SetChecksum();
  string getChecksumAlgorithm();
  void setChecksumAlgorithm();
}