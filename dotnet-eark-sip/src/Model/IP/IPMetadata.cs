using Mets;

namespace IP
{
  /// <summary>
  /// Represents metadata for an Intellectual Property (IP) object.
  /// </summary>
  [Serializable]
  public class IPMetadata
  {
    private string id;
    private DateTime? createDate;
    private IIPFile metadata;
    private MetadataType metadataType = new MetadataType(IMetadataMdtype.OTHER);
    private MetadataStatus metadataStatus = MetadataStatus.CURRENT;

    /// <summary>
    /// Initializes a new instance of the <see cref="IPMetadata"/>.
    /// </summary>
    /// <remarks>For serialization purposes</remarks>
    public IPMetadata()
    {
      id = "";
      metadata = new IPFile();
      // empty constructor for serialization purposes
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPMetadata"/> class with the specified metadata.
    /// </summary>
    /// <param name="metadata">The metadata file associated with the IP.</param>
    /// <remarks>Sets the metadata type as <see cref="IMetadataMdtype.OTHER"/></remarks>
    public IPMetadata(IIPFile metadata)
    {
      id = Utils.GenerateRandomAndPrefixedUUID();
      createDate = DateTime.Now;
      this.metadata = metadata;
      metadataType = new MetadataType(IMetadataMdtype.OTHER);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPMetadata"/> class with the specified metadata, and metadata type.
    /// </summary>
    /// <param name="metadata">The metadata file associated with the IP.</param>
    /// <param name="metadataType">The type of the metadata.</param>
    public IPMetadata(IIPFile metadata, MetadataType metadataType)
    {
      id = Utils.GenerateRandomAndPrefixedUUID();
      createDate = DateTime.Now;
      this.metadata = metadata;
      this.metadataType = metadataType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPMetadata"/> class with the specified ID, metadata, and metadata type.
    /// </summary>
    /// <param name="id">The unique identifier for the metadata.</param>
    /// <param name="metadata">The metadata file associated with the IP.</param>
    /// <param name="metadataType">The type of the metadata.</param>
    public IPMetadata(string id, IIPFile metadata, MetadataType metadataType)
    {
      this.id = id;
      createDate = DateTime.Now;
      this.metadata = metadata;
      this.metadataType = metadataType;
    }

    /// <summary>
    /// Gets the unique identifier for the metadata.
    /// </summary>
    /// <returns>The unique identifier as a string.</returns>
    public string GetID()
    {
      return id;
    }

    /// <summary>
    /// Sets the unique identifier for the metadata.
    /// </summary>
    /// <param name="id">The unique identifier to set.</param>
    /// <returns>The current instance of <see cref="IPMetadata"/>.</returns>
    public IPMetadata SetID(string id)
    {
      this.id = id;
      return this;
    }

    /// <summary>
    /// Gets the creation date for the metadata.
    /// </summary>
    /// <returns>The creation date.</returns>
    public DateTime? GetCreateDate()
    {
      return createDate;
    }

    /// <summary>
    /// Sets the creation date for the metadata.
    /// </summary>
    /// <param name="createDate">The creation date.</param>
    /// <returns>The current instance of <see cref="IPMetadata"/>.</returns>
    public IPMetadata SetCreateDate(DateTime? createDate)
    {
      this.createDate = createDate;
      return this;
    }

    /// <summary>
    /// Gets the metadata file associated with the IP.
    /// </summary>
    /// <returns>The metadata file.</returns>
    public IIPFile GetMetadata()
    {
      return metadata;
    }

    /// <summary>
    /// Sets the metadata file associated with the IP.
    /// </summary>
    /// <param name="metadata">The metadata file to set.</param>
    /// <returns>The current instance of <see cref="IPMetadata"/>.</returns>
    public IPMetadata SetMetadata(IIPFile metadata)
    {
      this.metadata = metadata;
      return this;
    }

    /// <summary>
    /// Gets the type of the metadata.
    /// </summary>
    /// <returns>The metadata type.</returns>
    public MetadataType GetMetadataType()
    {
      return metadataType;
    }

    /// <summary>
    /// Sets the metadata type using an <see cref="IMetadataMdtype"/> value.
    /// </summary>
    /// <param name="metadataType">The metadata type to set.</param>
    /// <returns>The current instance of <see cref="IPMetadata"/>.</returns>
    public IPMetadata SetMetadataType(IMetadataMdtype metadataType)
    {
      this.metadataType = new MetadataType(metadataType);
      return this;
    }

    /// <summary>
    /// Sets the metadata type using a <see cref="MetadataType"/> value.
    /// </summary>
    /// <param name="metadataType">The metadata type to set.</param>
    /// <returns>The current instance of <see cref="IPMetadata"/>.</returns>
    public IPMetadata SetMetadataType(MetadataType metadataType)
    {
      this.metadataType = metadataType;
      return this;
    }

    /// <summary>
    /// Gets the metadata status as a string.
    /// </summary>
    /// <returns>The metadata status in XML enum name format.</returns>
    public string GetMetadataStatus()
    {
      return EnumUtils.GetXmlEnumName(metadataStatus);
    }

    /// <summary>
    /// Sets the metadata status using a <see cref="MetadataStatus"/> value.
    /// </summary>
    /// <param name="metadataStatus">The metadata status to set.</param>
    /// <returns>The current instance of <see cref="IPMetadata"/>.</returns>
    public IPMetadata SetMetadataStatus(MetadataStatus metadataStatus)
    {
      this.metadataStatus = metadataStatus;
      return this;
    }

    /// <summary>
		/// Returns a string representation of the IPMetadata object.
		/// </summary>
		/// <returns>A string that represents the current IPMetadata object.</returns>
    public override string ToString()
    {
      return "IPMetadata [" +
        "id=" + id +
        ", createDate=" + createDate ?? " " +
        ", metadata=" + metadata +
        ", metadataType=" + metadataType +
        ", metadataStatus=" + GetMetadataStatus() +
      "]";
    }
  }
}