using System.Text;

namespace IP {
  [Serializable]
  public class IPContentInformationType {

    public enum IPContentInformationTypeEnum {
      ERMS, citserms_v2_1, SIARD1, SIARD2, SIARDDK, GEODATA, MIXED, OTHER, Dataset, citssiard_v1_0
    }

    private readonly IPContentInformationTypeEnum type;
    private string otherType;

    public IPContentInformationType(string type) {
      try {
        this.type = (IPContentInformationTypeEnum)Enum.Parse(typeof(IPContentInformationTypeEnum), type);
        otherType = "";
      } catch {
        this.type = IPContentInformationTypeEnum.OTHER;
        otherType = type;
      }
    }

    public IPContentInformationType(IPContentInformationTypeEnum type) {
      this.type = type;
      otherType = "";
    }

    public IPContentInformationTypeEnum GetContentInformationType() {
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
      return type == IPContentInformationTypeEnum.OTHER && !string.IsNullOrEmpty(otherType);
    }

    public static IPContentInformationType GetMIXED() {
      return new IPContentInformationType(IPContentInformationTypeEnum.MIXED);
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