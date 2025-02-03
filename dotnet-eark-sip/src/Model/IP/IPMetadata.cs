using Mets;

namespace IP {
  [Serializable]
  public class IPMetadata {
    private string id;
    private DateTime? createDate;
    private IIPFile metadata;
    private MetadataType metadataType = new MetadataType(IMetadataMdtype.OTHER);
    private MetadataStatus metadataStatus = MetadataStatus.CURRENT;

    public IPMetadata() {
      // empty constructor for serialization purposes
    }

    public IPMetadata(IIPFile metadata) {
      id = Utils.GenerateRandomAndPrefixedUUID();
      createDate = new DateTime();
      this.metadata = metadata;
      metadataType = new MetadataType(IMetadataMdtype.OTHER);
    }

    public IPMetadata(IIPFile metadata, MetadataType metadataType) {
      id = Utils.GenerateRandomAndPrefixedUUID();
      createDate = new DateTime();
      this.metadata = metadata;
      this.metadataType = metadataType;
    }

    public IPMetadata(string id, IIPFile metadata, MetadataType metadataType) {
      this.id = id;
      createDate = new DateTime();
      this.metadata = metadata;
      this.metadataType = metadataType;
    }

    public string GetID() {
      return id;
    }

    public IPMetadata SetID(string id) {
      this.id = id;
      return this;
    }

    public DateTime? GetCreateDate() {
      return createDate;
    }

    public IPMetadata SetCreateDate(DateTime? createDate) {
      this.createDate = createDate;
      return this;
    }

    public IIPFile GetMetadata() {
      return metadata;
    }

    public IPMetadata SetMetadata(IIPFile metadata) {
      this.metadata = metadata;
      return this;
    }

    public MetadataType GetMetadataType() {
      return metadataType;
    }

    public IPMetadata SetMetadataType(IMetadataMdtype metadataType) {
      this.metadataType = new MetadataType(metadataType);
      return this;
    }

    public IPMetadata SetMetadataType(MetadataType metadataType) {
      this.metadataType = metadataType;
      return this;
    }

    public string getMetadataStatus() {
      return metadataStatus.ToString();
    }

    public IPMetadata setMetadataStatus(MetadataStatus metadataStatus) {
      this.metadataStatus = metadataStatus;
      return this;
    }

    public override string ToString()
    {
      return "IPMetadata [id=" + id + ", createDate=" + createDate + ", metadata=" + metadata + ", metadataType=" + metadataType + ", metadataStatus=" + metadataStatus + "]";
    }
  }
}