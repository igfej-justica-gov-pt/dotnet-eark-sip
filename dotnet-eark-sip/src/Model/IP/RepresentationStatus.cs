using System.Text;

namespace IP {
  [Serializable]
  public class RepresentationStatus {
    public enum RepresentationStatusEnum {
      ORIGINAL, OTHER
    }

    public RepresentationStatusEnum Status { get; private set; }
    public string OtherStatus { get; private set; }

    public RepresentationStatus(string status) {
      try {
        Status = (RepresentationStatusEnum)Enum.Parse(typeof(RepresentationStatusEnum), status);
        OtherStatus = string.Empty;
      } catch {
        Status = RepresentationStatusEnum.OTHER;
        OtherStatus = status;
      }
    }

    public RepresentationStatus(RepresentationStatusEnum status) {
      Status = status;
      OtherStatus = string.Empty;
    }

    public static RepresentationStatus GetORIGINAL() {
      return new RepresentationStatus(RepresentationStatusEnum.ORIGINAL);
    }

    public static RepresentationStatus GetOTHER() {
      return new RepresentationStatus(RepresentationStatusEnum.OTHER);
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.Append("status: ").Append(Status);
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
}