namespace IP {
  [Serializable]
  public class IPDescriptiveMetadata : IPMetadata {
    public string MetadataVersion { set; get; }

    public IPDescriptiveMetadata(IIPFile metadata, MetadataType metadataType, string metadataVersion) : base(metadata, metadataType) {
      MetadataVersion = metadataVersion;
    }

    public IPDescriptiveMetadata(string id, IIPFile metadata, MetadataType metadataType, string metadataVersion) : base(id, metadata, metadataType) {
      MetadataVersion = metadataVersion;
    }

    public override string ToString() {
      return "IPDescriptiveMetadata [metadataVersion=" + MetadataVersion + ", getMetadata()=" + GetMetadata() + ", getMetadataType()=" + GetMetadataType() + "]";
    }
  }
}