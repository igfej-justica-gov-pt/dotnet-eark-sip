using System.Text;
using System.Xml.Serialization;

namespace IP {
  [Serializable]
  public class IPContentType {
    private readonly IPContentTypeEnum type;
    private string otherType;

    public IPContentType(string type, string? otherType = null) {
      try {
        this.type = EnumUtils.GetEnumFromXmlAttribute<IPContentTypeEnum>(type);
        this.otherType = otherType ?? "";
      } catch {
        this.type = IPContentTypeEnum.OTHER;
        this.otherType = type;
      }
    }

    public IPContentType(IPContentTypeEnum type, string? otherType = null) {
      this.type = type;
      this.otherType = otherType ?? "";
    }

    public IPContentTypeEnum GetContentType() {
      return type;
    }

    public string GetOtherType() {
      return otherType;
    }

    public IPContentType SetOtherType(string otherType) {
      this.otherType = otherType;
      return this;
    }

    public bool IsOtherAndOtherTypeIsDefined() {
      return type == IPContentTypeEnum.OTHER && !string.IsNullOrEmpty(otherType);
    }

    public static IPContentType GetMIXED() {
      return new IPContentType(IPContentTypeEnum.MIXED);
    }

    public static IPContentType GetDataset() {
      return new IPContentType(IPContentTypeEnum.DATASET);
    }

    public static IPContentType GetDatabase() {
      return new IPContentType(IPContentTypeEnum.DATABASES);
    }

    public string AsString() {
      string res = EnumUtils.GetXmlEnumName(type);

      if (type == IPContentTypeEnum.OTHER && !string.IsNullOrEmpty(otherType)) {
        res = otherType;
      }

      return res;
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

  public enum IPContentTypeEnum {
    [XmlEnum("Textual works – Print")]
    TEXTUAL_WORKS_PRINT,

    [XmlEnum("Textual works – Digital")]
    TEXTUAL_WORKS_DIGITAL,

    [XmlEnum("Textual works – Electronic Serials")]
    TEXTUAL_WORKS_ELECTRONIC_SERIALS,

    [XmlEnum("Digital Musical Composition (score-based representations)")]
    DIGITAL_MUSICAL_COMPOSITION,

    [XmlEnum("Photographs – Print")]
    PHOTOGRAPHS_PRINT,

    [XmlEnum("Photographs – Digital")]
    PHOTOGRAPHS_DIGITAL,

    [XmlEnum("Other Graphic Images – Print")]
    OTHER_GRAPHIC_IMAGES_PRINT,

    [XmlEnum("Other Graphic Images – Digital")]
    OTHER_GRAPHIC_IMAGES_DIGITAL,

    [XmlEnum("Microforms")]
    MICROFORMS,

    [XmlEnum("Audio – On Tangible Medium (digital or analog)")]
    AUDIO_ON_TANGIBLE_MEDIUM,

    [XmlEnum("Audio – Media-independent (digital)")]
    AUDIO_MEDIA_INDEPENDENT,

    [XmlEnum("Motion Pictures – Digital and Physical Media")]
    MOTION_PICTURES,

    [XmlEnum("Video – File-based and Physical Media")]
    VIDEO,

    [XmlEnum("Software")]
    SOFTWARE,

    [XmlEnum("Datasets")]
    DATASETS,

    [XmlEnum("Dataset")]
    DATASET,

    [XmlEnum("Geospatial Data")]
    GEOSPATIAL_DATA,

    [XmlEnum("Databases")]
    DATABASES,

    [XmlEnum("Websites")]
    WEBSITES,

    [XmlEnum("Collection")]
    COLLECTION,

    [XmlEnum("Event")]
    EVENT,

    [XmlEnum("Interactive resource")]
    INTERACTIVE_RESOURCE,

    [XmlEnum("Physical object")]
    PHYSICAL_OBJECT,

    [XmlEnum("Service")]
    SERVICE,

    [XmlEnum("Mixed")]
    MIXED,

    [XmlEnum("Other")]
    OTHER,
  }
}