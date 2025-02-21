using IP;
using Mets;

public class IPFileShallow : IIPFile {
  public Uri FileLocation { get; set; }
  public FileType FileType { get; set; }
  private List<string> relativeFolders = new List<string>();

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  public IPFileShallow(List<string> relativeFolders) {
    this.relativeFolders = relativeFolders;
  }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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

  public override string ToString() {
    return "IPFileShallow[" +
      "FileLocation=" + FileLocation ?? " " +
      ", FileType=" + FileType +
      ", RelativeFolders=[" + string.Join(", ", relativeFolders) + "]" +
    "]";
  }
}