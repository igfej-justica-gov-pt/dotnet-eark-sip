public interface IWriteStrategy {
  void Setup(string destinationPath);

  string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string fileNameWithoutExtension, string fallbackName, bool deleteExisting);

  string Write(Dictionary<string, IZipEntryInfo> entries, SIP sip, string fileNameWithoutExtension, string fallbackName, bool createSipIdFolder, bool deleteExisting);

  string GetDestinationPath();
}