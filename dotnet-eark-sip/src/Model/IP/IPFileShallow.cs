using IP;
using Mets;

public class IPFileShallow : IIPFile {
  public Uri FileLocation { get; set; } = new Uri("");
  public FileType FileType { get; set; } = new FileType();
  private List<string> relativeFolders = new List<string>();

  public IPFileShallow(List<string> relativeFolders) {
    this.relativeFolders = relativeFolders;
  }

  public IPFileShallow(Uri fileLocation, FileType fileType) : base() {
    FileLocation = fileLocation;
    FileType = fileType;
  }

  public IPFileShallow(Uri fileLocation, FileType fileType, List<string> relativeFolders) : base() {
    FileLocation = fileLocation;
    FileType = fileType;
    this.relativeFolders = relativeFolders;
  }

  public List<string> GetRelativeFolders() {
    return relativeFolders;
  }

  public IIPFile SetRelativeFolders(List<string> relativeFolders) {
    this.relativeFolders = relativeFolders;
    return this;
  }

  public static IPFileShallow CreateEmptyFolder(List<string> emptyFolderPath) {
    return new IPFileShallow(emptyFolderPath);
  }

  public string? GetFileName() => null;

  public string GetPath() {
    throw new NotImplementedException("IPFileShallow does not support this method");
  }
}