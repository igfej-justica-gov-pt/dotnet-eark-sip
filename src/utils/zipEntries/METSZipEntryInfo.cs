using Mets;

public class METSZipEntryInfo : FileZipEntryInfo {
  private readonly Mets.Mets mets;
  private readonly bool mainMets;
  public Dictionary<string, string> Checksums { get; set; }
  public long Size { get; set; }
  private readonly FileType fileType;

  public METSZipEntryInfo(string name, string filePath, Mets.Mets mets, bool mainMets, FileType fileType) : base(name, filePath) {
    this.mets = mets;
    this.mainMets = mainMets;
    Checksums = new Dictionary<string, string>();
    Size = 0;
    this.fileType = fileType;
  }

  public override void PrepareEntryForZipping() {
    try {
      METSUtils.MarshallMETS(mets, FilePath, mainMets);
      if (!mainMets && fileType != null) {
        METSUtils.SetFileBasicInformation(null, FilePath, fileType);

        string checksumType = Checksum;
        HashSet<string> checksumAlgorithms = new HashSet<string>();
        using (FileStream inputStream = File.Create(FilePath)) {
          Dictionary<string, string> checksums = ZIPUtils.CalculateChecksums(null, inputStream, checksumAlgorithms);
          string checksum = Checksums[checksumType];
          fileType.Checksum = checksum;
          fileType.Checksumtype = (IFilecoreChecksumtype)Enum.Parse(typeof(IFilecoreChecksumtype), checksumType);
        }
      }
    } catch (Exception e) {
      throw new IPException("Error marshalling METS", e);
    }
  }
}