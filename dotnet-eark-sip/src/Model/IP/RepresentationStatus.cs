using System.Text;
using System.Xml.Serialization;

namespace IP {
  [Serializable]
  public class RepresentationStatus {
    public RepresentationStatusEnum Status { get; private set; }
    public string OtherStatus { get; private set; }

    public RepresentationStatus(string status, string? otherStatus = null) {
      try {
        Status = EnumUtils.GetEnumFromXmlAttribute<RepresentationStatusEnum>(status);
        OtherStatus = otherStatus ?? "";
      } catch {
        Status = RepresentationStatusEnum.OTHER;
        OtherStatus = status;
      }
    }

    public RepresentationStatus(RepresentationStatusEnum status, string? otherStatus = null) {
      Status = status;
      OtherStatus = otherStatus ?? "";
    }

    public static RepresentationStatus GetORIGINAL() {
      return new RepresentationStatus(RepresentationStatusEnum.ORIGINAL);
    }

    public static RepresentationStatus GetOTHER() {
      return new RepresentationStatus(RepresentationStatusEnum.OTHER);
    }

    public string AsString() {
      string result = EnumUtils.GetXmlEnumName(Status);

      if (Status == RepresentationStatusEnum.OTHER && !string.IsNullOrEmpty(OtherStatus)) {
        result = OtherStatus;
      }

      return result;
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.Append("status: ").Append(EnumUtils.GetXmlEnumName(Status));
      if (!string.IsNullOrEmpty(OtherStatus)) {
        sb.Append("; otherStatus: ").Append(OtherStatus);
      }

      return sb.ToString();
    }

    public override int GetHashCode() {
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

  public enum RepresentationStatusEnum {
    [XmlEnum("ORIGINAL")]
    ORIGINAL,

    [XmlEnum("OTHER")]
    OTHER
  }
}