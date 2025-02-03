public interface IWriteStrategy {
  string DestinationPath { get; }

  void Setup(string destinationPath);

  string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string fileNameWithoutExtension, string fallbackName, bool deleteExisting);
  string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string fileNameWithoutExtension, string fallbackName, bool deleteExisting, bool createSipIdFolder);
}