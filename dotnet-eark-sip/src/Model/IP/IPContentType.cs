using System.Text;

namespace IP {
  [Serializable]
  public class IPContentType {
    public enum IPContentTypeEnum {
      [System.Xml.Serialization.XmlEnum("Textual works – Print")]
      TEXTUAL_WORKS_PRINT,
      
      [System.Xml.Serialization.XmlEnum("Textual works – Digital")]
      TEXTUAL_WORKS_DIGITAL,
      
      [System.Xml.Serialization.XmlEnum("Textual works – Electronic Serials")]
      TEXTUAL_WORKS_ELECTRONIC_SERIALS,
      
      [System.Xml.Serialization.XmlEnum("Digital Musical Composition (score-based representations)")]
      DIGITAL_MUSICAL_COMPOSITION,
      
      [System.Xml.Serialization.XmlEnum("Photographs – Print")]
      PHOTOGRAPHS_PRINT,
      
      [System.Xml.Serialization.XmlEnum("Photographs – Digital")]
      PHOTOGRAPHS_DIGITAL,
      
      [System.Xml.Serialization.XmlEnum("Other Graphic Images – Print")]
      OTHER_GRAPHIC_IMAGES_PRINT,
      
      [System.Xml.Serialization.XmlEnum("Other Graphic Images – Digital")]
      OTHER_GRAPHIC_IMAGES_DIGITAL,
      
      [System.Xml.Serialization.XmlEnum("Microforms")]
      MICROFORMS,
      
      [System.Xml.Serialization.XmlEnum("Audio – On Tangible Medium (digital or analog)")]
      AUDIO_ON_TANGIBLE_MEDIUM,
      
      [System.Xml.Serialization.XmlEnum("Audio – Media-independent (digital)")]
      AUDIO_MEDIA_INDEPENDENT,
      
      [System.Xml.Serialization.XmlEnum("Motion Pictures – Digital and Physical Media")]
      MOTION_PICTURES,
      
      [System.Xml.Serialization.XmlEnum("Video – File-based and Physical Media")]
      VIDEO,
      
      [System.Xml.Serialization.XmlEnum("Software")]
      SOFTWARE,
      
      [System.Xml.Serialization.XmlEnum("Datasets")]
      DATASETS,
      
      [System.Xml.Serialization.XmlEnum("Dataset")]
      DATASET,
      
      [System.Xml.Serialization.XmlEnum("Geospatial Data")]
      GEOSPATIAL_DATA,
      
      [System.Xml.Serialization.XmlEnum("Databases")]
      DATABASES,
      
      [System.Xml.Serialization.XmlEnum("Websites")]
      WEBSITES,
      
      [System.Xml.Serialization.XmlEnum("Collection")]
      COLLECTION,
      
      [System.Xml.Serialization.XmlEnum("Event")]
      EVENT,
      
      [System.Xml.Serialization.XmlEnum("Interactive resource")]
      INTERACTIVE_RESOURCE,
      
      [System.Xml.Serialization.XmlEnum("Physical object")]
      PHYSICAL_OBJECT,
      
      [System.Xml.Serialization.XmlEnum("Service")]
      SERVICE,
      
      [System.Xml.Serialization.XmlEnum("Mixed")]
      MIXED,
      
      [System.Xml.Serialization.XmlEnum("Other")]
      OTHER
    }

    private readonly IPContentTypeEnum type;
    private string otherType;

    public IPContentType(string type) {
      try {
        this.type = (IPContentTypeEnum)Enum.Parse(typeof(IPContentTypeEnum), type);
        otherType = string.Empty;
      } catch {
        this.type = IPContentTypeEnum.OTHER;
        otherType = type;
      }
    }

    public IPContentType(IPContentTypeEnum type) {
      this.type = type;
      otherType = string.Empty;
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
      string res = type.ToString();

      if (type == IPContentTypeEnum.OTHER && !string.IsNullOrEmpty(otherType)) {
        res = otherType;
      }

      return res;
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