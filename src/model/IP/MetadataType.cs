using System.Text;
using Mets;

namespace IP {
  [Serializable]
  public class MetadataType {
    private readonly IMetadataMdtype type;
    private string otherType;

    public MetadataType(string type) {
      try {
        this.type = (IMetadataMdtype)Enum.Parse(typeof(IMetadataMdtype), type);
        otherType = string.Empty;
      } catch {
        this.type = IMetadataMdtype.OTHER;
        otherType = type;
      }
    }

    public MetadataType(IMetadataMdtype type) {
      this.type = type;
      otherType = string.Empty;
    }

    public IMetadataMdtype _GetType() {
      return type;
    }

    public string GetOtherType() {
      return otherType;
    }

    public MetadataType SetOtherType(string otherType) {
      this.otherType = otherType;
      return this;
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.Append("type: ").Append(type);
      if (string.IsNullOrEmpty(otherType)) {
        sb.Append("; othertype: ").Append(otherType);
      }

      return sb.ToString();
    }

    public override int GetHashCode() {
      int prime = 31;
      int result = 1;

      result = prime * result + ((otherType == null) ? 0 : otherType.GetHashCode());
      result = prime * result + type.GetHashCode();

      return result;
    }

    public override bool Equals(object obj) {
      if (this == obj) return true;
      if (obj == null) return false;
      if (!(obj is MetadataType other)) return false;

      return this.type == other._GetType() && this.otherType.Equals(other.GetOtherType());
    }
  }
}