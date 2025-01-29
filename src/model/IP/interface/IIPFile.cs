namespace IP {
  public interface IIPFile {
    List<string> GetRelativeFolders();
    string GetFileName();
    string GetPath();
  }
}