using System.Text;
using Xml.Mets.CsipExtensionMets;

namespace IP {
  [Serializable]
  public class IPContentInformationType {
    private readonly Contentinformationtype type;
    private string otherType;

    public IPContentInformationType(string type, string? otherType = null) {
      try {
        this.type = EnumUtils.GetEnumFromXmlAttribute<Contentinformationtype>(type);
        this.otherType = otherType ?? "";
      } catch {
        this.type = Contentinformationtype.OTHER;
        this.otherType = type;
      }
    }

    public IPContentInformationType(Contentinformationtype type, string? otherType = null) {
      this.type = type;
      this.otherType = otherType ?? "";
    }

    public Contentinformationtype GetContentInformationType() {
      return type;
    }

    public string GetOtherType() {
      return otherType;
    }

    public IPContentInformationType SetOtherType(string otherType) {
      this.otherType = otherType;
      return this;
    }

    public bool IsOtherAndOtherTypeIsDefined() {
      return type == Contentinformationtype.OTHER && !string.IsNullOrEmpty(otherType);
    }

    public static IPContentInformationType GetMIXED() {
      return new IPContentInformationType(Contentinformationtype.MIXED);
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder();
      sb.Append("type: ").Append(EnumUtils.GetXmlEnumName(type));

      if (!string.IsNullOrEmpty(otherType)) {
        sb.Append("; otherType: ").Append(otherType);
      }

      return sb.ToString();
    }
  }
}