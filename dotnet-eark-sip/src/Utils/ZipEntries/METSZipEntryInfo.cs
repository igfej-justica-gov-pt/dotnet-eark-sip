using Mets;

public class METSZipEntryInfo : FileZipEntryInfo {
  private readonly Mets.Mets mets;
  private readonly bool mainMets;
  public Dictionary<IFilecoreChecksumtype, string> Checksums { get; set; }
  public long Size { get; set; }
  private readonly FileType fileType;

  public METSZipEntryInfo(string name, string filePath, Mets.Mets mets, bool mainMets, FileType fileType) : base(name, filePath) {
    this.mets = mets;
    this.mainMets = mainMets;
    Checksums = new Dictionary<IFilecoreChecksumtype, string>();
    Size = 0;
    this.fileType = fileType;
  }

  public override void PrepareEntryForZipping() {
    try {
      METSUtils.MarshallMETS(mets, FilePath, mainMets);
      if (!mainMets && fileType != null) {
        // TODO: Add logger
        METSUtils.SetFileBasicInformation(FilePath, fileType);

        IFilecoreChecksumtype checksumType = ChecksumAlgorithm;
        HashSet<IFilecoreChecksumtype> checksumAlgorithms = new HashSet<IFilecoreChecksumtype> { checksumType };
        using (FileStream inputStream = File.Create(FilePath)) {
          Dictionary<IFilecoreChecksumtype, string> checksums = ZIPUtils.CalculateChecksums(null, inputStream, checksumAlgorithms);
          string checksum = checksums[checksumType];
          fileType.Checksum = checksum;
          fileType.Checksumtype = checksumType;
        }
      }
    } catch (Exception e) {
      throw new IPException("Error marshalling METS", e);
    }
  }
}