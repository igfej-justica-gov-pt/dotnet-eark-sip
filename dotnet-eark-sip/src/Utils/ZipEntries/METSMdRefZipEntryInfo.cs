using Mets;

public class METSMdRefZipEntryInfo : FileZipEntryInfo {
  public MdSecTypeMdRef MetsMdRef { get; set; }

  public METSMdRefZipEntryInfo(string name, string filePath) : base(name, filePath) {}

  public METSMdRefZipEntryInfo(string name, string filePath, MdSecTypeMdRef mdRef) : base(name, filePath) {
    MetsMdRef = mdRef;
  }
}