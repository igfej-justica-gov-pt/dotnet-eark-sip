using Mets;

public class METSMdRefZipEntryInfo : FileZipEntryInfo {
  public MdSecTypeMdRef MetsMdRef { get; set; }

  public METSMdRefZipEntryInfo(string name, string filePath) : base(name, filePath) {
    MetsMdRef = new MdSecTypeMdRef();
  }

  public METSMdRefZipEntryInfo(string name, string filePath, MdSecTypeMdRef mdRef) : base(name, filePath) {
    MetsMdRef = mdRef;
  }
}