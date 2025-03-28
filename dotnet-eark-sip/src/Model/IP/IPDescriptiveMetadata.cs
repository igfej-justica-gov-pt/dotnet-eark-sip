namespace IP
{
  /// <summary>
  /// Represents descriptive metadata for an IP, including metadata version and type.
  /// </summary>
  [Serializable]
  public class IPDescriptiveMetadata : IPMetadata
  {
    /// <summary>
    /// Gets or sets the version of the metadata.
    /// </summary>
    public string? MetadataVersion { set; get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPDescriptiveMetadata"/> class with the metadata, metadata type, and metadata version.
    /// </summary>
    /// <param name="metadata">The metadata file associated with this instance.</param>
    /// <param name="metadataType">The type of the metadata.</param>
    /// <param name="metadataVersion">The version of the metadata.</param>
    public IPDescriptiveMetadata(IIPFile metadata, MetadataType metadataType, string? metadataVersion) : base(metadata, metadataType)
    {
      MetadataVersion = metadataVersion;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPDescriptiveMetadata"/> class with the specified ID, metadata, metadata type, and metadata version.
    /// </summary>
    /// <param name="id">The unique identifier for the metadata.</param>
    /// <param name="metadata">The metadata file associated with this instance.</param>
    /// <param name="metadataType">The type of the metadata.</param>
    /// <param name="metadataVersion">The version of the metadata.</param>
    public IPDescriptiveMetadata(string id, IIPFile metadata, MetadataType metadataType, string? metadataVersion) : base(id, metadata, metadataType)
    {
      MetadataVersion = metadataVersion;
    }

    /// <summary>
    /// Returns a string representation of the <see cref="IPDescriptiveMetadata"/> instance.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
      return "IPDescriptiveMetadata [" +
        "metadataVersion=" + MetadataVersion ?? " " +
        ", getMetadata()=" + GetMetadata() +
        ", getMetadataType()=" + GetMetadataType() +
      "]";
    }
  }
}