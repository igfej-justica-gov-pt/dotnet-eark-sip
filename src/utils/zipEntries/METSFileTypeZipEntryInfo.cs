using Mets;

public class METSFileTypeZipEntryInfo : FileZipEntryInfo {
  private FileType MetsFileType { get; set; }

  public METSFileTypeZipEntryInfo(string name, string filePath) : base(name, filePath) {}

  public METSFileTypeZipEntryInfo(string name, string filePath, FileType fileType) : base(name, filePath) {
    MetsFileType = fileType;
  }
}