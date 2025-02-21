using Mets;

namespace IP {
  public class IPFile : IIPFile {
    private DirectoryInfo path;
    private string? renameTo;
    private List<string> relativeFolders = new List<string>();
    private string? checksum;
    private IFilecoreChecksumtype? checksumAlgorithm;
    private List<string> relatedTags = new List<string>();

    public IPFile() : base() {
      path = new DirectoryInfo("");
    }

    public IPFile(string path) : base() {
      this.path = new DirectoryInfo(path);
    }

    public IPFile(string path, List<string> relativeFolders) : base() {
      this.path = new DirectoryInfo(path);
      this.relativeFolders = relativeFolders;
    }

    public IPFile(string path, string renameTo) : base() {
      this.path = new DirectoryInfo(path);
      this.renameTo = renameTo;
    }

    public string GetPath() {
      return path.FullName;
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

    public string? GetRenameTo() {
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

    public string? GetChecksum() {
      return checksum;
    }

    public IIPFile SetChecksum(string checksum) {
      this.checksum = checksum;
      return this;
    }

    public IIPFile SetChecksum(string checksum, IFilecoreChecksumtype? checksumAlgorithm = null) {
      this.checksum = checksum ?? "";
      this.checksumAlgorithm = checksumAlgorithm;
      return this;
    }

    public IFilecoreChecksumtype? GetChecksumAlgorithm() {
      return checksumAlgorithm;
    }

    public IIPFile SetChecksumAlgorithm(IFilecoreChecksumtype checksumAlgorithm) {
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

    private string GetChecksumAlgorithmString() {
      IFilecoreChecksumtype value = checksumAlgorithm ?? IFilecoreChecksumtype.SHA256;

      if (checksumAlgorithm == null) return " ";
      else return EnumUtils.GetXmlEnumName(value);
    }

    public override string ToString()
    {
      return "IPFile [" + 
        "path=" + path +
        ", renameTo=" + renameTo ?? " " +
        ", relativeFolders=[" + string.Join(",", relativeFolders) + "]" +
        ", checksum=" + checksum ?? " " +
        ", checksumAlgorithm=" + GetChecksumAlgorithmString() +
        ", relatedTags=[" + string.Join(",", relatedTags) + "]" +
      "]";
    }
  }
}