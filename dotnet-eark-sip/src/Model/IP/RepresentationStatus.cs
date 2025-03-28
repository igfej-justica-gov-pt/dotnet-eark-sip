using System.Text;
using System.Xml.Serialization;

namespace IP
{
  /// <summary>
  /// Represents the status of a representation, including predefined and custom statuses.
  /// </summary>
  [Serializable]
  public class RepresentationStatus
  {
    /// <summary>
    /// Gets the predefined status of the representation.
    /// </summary>
    public RepresentationStatusEnum Status { get; private set; }
    /// <summary>
    /// Gets the custom status of the representation if the predefined statuses do not apply.
    /// </summary>
    public string OtherStatus { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RepresentationStatus"/> class with a status and an optional custom status.
    /// </summary>
    /// <param name="status">The predefined status of the representation.</param>
    /// <param name="otherStatus">The custom status of the representation, if applicable.</param>
    public RepresentationStatus(string status, string? otherStatus = null)
    {
      try
      {
        Status = EnumUtils.GetEnumFromXmlAttribute<RepresentationStatusEnum>(status);
        OtherStatus = otherStatus ?? "";
      }
      catch
      {
        Status = RepresentationStatusEnum.OTHER;
        OtherStatus = status;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RepresentationStatus"/> class with a status and an optional custom status.
    /// </summary>
    /// <param name="status">The predefined status of the representation.</param>
    /// <param name="otherStatus">The custom status of the representation, if applicable.</param>
    public RepresentationStatus(RepresentationStatusEnum status, string? otherStatus = null)
    {
      Status = status;
      OtherStatus = otherStatus ?? "";
    }

    /// <summary>
    /// Gets a <see cref="RepresentationStatus"/> instance representing the "ORIGINAL" status.
    /// </summary>
    public static RepresentationStatus GetORIGINAL()
    {
      return new RepresentationStatus(RepresentationStatusEnum.ORIGINAL);
    }

    /// <summary>
    /// Gets a <see cref="RepresentationStatus"/> instance representing the "OTHER" status.
    /// </summary>
    public static RepresentationStatus GetOTHER()
    {
      return new RepresentationStatus(RepresentationStatusEnum.OTHER);
    }

    public string AsString()
    {
      string result = EnumUtils.GetXmlEnumName(Status);

      if (Status == RepresentationStatusEnum.OTHER && !string.IsNullOrEmpty(OtherStatus))
      {
        result = OtherStatus;
      }

      return result;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("status: ").Append(EnumUtils.GetXmlEnumName(Status));
      if (!string.IsNullOrEmpty(OtherStatus))
      {
        sb.Append("; otherStatus: ").Append(OtherStatus);
      }

      return sb.ToString();
    }

    public override int GetHashCode()
    {
      int prime = 31;
      int result = 1;
      result = prime * result + ((OtherStatus == null) ? 0 : OtherStatus.GetHashCode());
      result = prime * result + Status.GetHashCode();
      return result;
    }

    public override bool Equals(object obj)
    {
      if (this == obj) return true;
      if (obj == null) return false;
      if (!(obj is RepresentationStatus other)) return false;

      return Status == other.Status && OtherStatus.Equals(other.OtherStatus);
    }
  }

  /// <summary>
  /// Enum representing the predefined statuses of a representation.
  /// </summary>
  public enum RepresentationStatusEnum
  {
    /// <summary>
    /// Represents the original status of a representation.
    /// </summary>
    [XmlEnum("ORIGINAL")]
    ORIGINAL,

    /// <summary>
    /// Represents a custom or undefined status of a representation.
    /// </summary>
    [XmlEnum("OTHER")]
    OTHER
  }
}