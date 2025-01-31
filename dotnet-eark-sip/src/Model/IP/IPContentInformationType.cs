using System.Text;
using Xml.Mets.CsipExtensionMets;

namespace IP {
  [Serializable]
  public class IPContentInformationType {

    private readonly Contentinformationtype type;
    private string otherType;

    public IPContentInformationType(string type) {
      try {
        this.type = (Contentinformationtype)Enum.Parse(typeof(Contentinformationtype), type);
        otherType = string.Empty;
      } catch {
        this.type = Contentinformationtype.OTHER;
        otherType = type;
      }
    }

    public IPContentInformationType(Contentinformationtype type) {
      this.type = type;
      otherType = string.Empty;
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

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("type: ").Append(type.ToString());

      if (!string.IsNullOrEmpty(otherType)) {
        sb.Append("; otherType: ").Append(otherType);
      }

      return sb.ToString();
    }
  }
}