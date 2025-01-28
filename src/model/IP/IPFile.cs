namespace IP {
  public class IPFile : IIPFile {
    private DirectoryInfo path;
    private string renameTo;
    private List<string> relativeFolders;
    private string checksum;
    private string checksumAlgorithm;
    private List<string> relatedTags;

    public IPFile() : base() {}

    public IPFile(string path) : base() {
      this.path = new DirectoryInfo(path);
      renameTo = null;
      relativeFolders = new List<string>();
      relatedTags = new List<string>();
    }

    public IPFile(string path, List<string> relativeFolders) : base() {
      this.path = new DirectoryInfo(path);
      renameTo = null;
      this.relativeFolders = relativeFolders;
      relatedTags = new List<string>();
    }

    public IPFile(string path, string renameTo) : base() {
      this.path = new DirectoryInfo(path);
      this.renameTo = renameTo;
      relativeFolders = new List<string>();
      relatedTags = new List<string>();
    }

    public DirectoryInfo GetPath() {
      return path;
    }

    public IPFile SetPath(string path) {
      this.path = new DirectoryInfo(path);
      return this;
    }

    public List<string> GetRelativeFolders() {
      return relativeFolders;
    }

    public IIPFile SetRelativeFolders(List<string> relativeFolders) {
      this.relativeFolders = relativeFolders;
      return this;
    }

    public string GetRenameTo() {
      return renameTo;
    }

    public IIPFile SetRenameTo(string renameTo) {
      this.renameTo = renameTo;
      return this;
    }

    public string GetFileName() {
      string filename;

      if (renameTo != null) {
        filename = renameTo;
      } else {
        filename = path.Name;
      }

      return filename;
    }

    public string GetChecksum() {
      return checksum;
    }

    public IIPFile SetChecksum(string checksum) {
      this.checksum = checksum;
      return this;
    }

    public IIPFile SetChecksum(string checksum, string checksumAlgorithm) {
      this.checksum = checksum ?? "";
      this.checksumAlgorithm = checksumAlgorithm ?? "";
      return this;
    }

    public string GetChecksumAlgorithm() {
      return checksumAlgorithm;
    }

    public IIPFile SetChecksumAlgorithm(string checksumAlgorithm) {
      this.checksumAlgorithm = checksumAlgorithm;
      return this;
    }

    public List<string> GetRelatedTags() {
      return relatedTags;
    }

    public IIPFile SetRelatedTags(List<string> relatedTags) {
      this.relatedTags = relatedTags;
      return this;
    }

    public override string ToString()
    {
      return "IPFile [path=" + path +
        ", renameTo=" + renameTo +
        ", relativeFolders=" + relativeFolders +
        ", checksum=" + checksum +
        ", checksumAlgorithm=" + checksumAlgorithm +
        ", relatedTags=" + relatedTags +
      "]";
    }
  }
}