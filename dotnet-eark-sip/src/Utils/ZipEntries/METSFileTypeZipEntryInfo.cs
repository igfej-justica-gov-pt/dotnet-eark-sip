using Mets;

public class METSFileTypeZipEntryInfo : FileZipEntryInfo {
  public FileType MetsFileType { get; set; }

  public METSFileTypeZipEntryInfo(string name, string filePath) : base(name, filePath) {}

  public METSFileTypeZipEntryInfo(string name, string filePath, FileType fileType) : base(name, filePath) {
    MetsFileType = fileType;
  }
}