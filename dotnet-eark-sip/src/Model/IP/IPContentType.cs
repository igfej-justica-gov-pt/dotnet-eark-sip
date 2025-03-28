using System.Text;
using System.Xml.Serialization;

namespace IP
{
  /// <summary>
  /// Represents the content type of an intellectual property (IP) object.
  /// </summary>
  [Serializable]
  public class IPContentType
  {
    private readonly IPContentTypeEnum type;
    private string otherType;

    /// <summary>
    /// Initializes a new instance of the <see cref="IPContentType"/> class with the specified type and optional other type.
    /// </summary>
    /// <param name="type">The content type as an enumeration value.</param>
    /// <param name="otherType">The other type as a string, if applicable.</param>
    public IPContentType(string type, string? otherType = null)
    {
      try
      {
        this.type = EnumUtils.GetEnumFromXmlAttribute<IPContentTypeEnum>(type);
        this.otherType = otherType ?? "";
      }
      catch
      {
        this.type = IPContentTypeEnum.OTHER;
        this.otherType = type;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPContentType"/> class with the specified type and optional other type.
    /// </summary>
    /// <param name="type">The content type as an enumeration value.</param>
    /// <param name="otherType">The other type as a string, if applicable.</param>
    public IPContentType(IPContentTypeEnum type, string? otherType = null)
    {
      this.type = type;
      this.otherType = otherType ?? "";
    }

    public IPContentTypeEnum GetContentType()
    {
      return type;
    }

    public string GetOtherType()
    {
      return otherType;
    }

    public IPContentType SetOtherType(string otherType)
    {
      this.otherType = otherType;
      return this;
    }

    /// <summary>
    /// Determines whether the content type is 'OTHER' and the 'otherType' field is defined.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the content type is 'OTHER' and 'otherType' is not null or empty; otherwise, <c>false</c>.
    /// </returns>
    public bool IsOtherAndOtherTypeIsDefined()
    {
      return type == IPContentTypeEnum.OTHER && !string.IsNullOrEmpty(otherType);
    }

    /// <summary>
    /// Gets an instance of IPContentType with the type set to MIXED.
    /// </summary>
    public static IPContentType GetMIXED()
    {
      return new IPContentType(IPContentTypeEnum.MIXED);
    }

    /// <summary>
    /// Gets an instance of IPContentType with the type set to DATASET.
    /// </summary>
    public static IPContentType GetDataset()
    {
      return new IPContentType(IPContentTypeEnum.DATASET);
    }

    /// <summary>
    /// Gets an instance of IPContentType with the type set to DATABASE.
    /// </summary>
    public static IPContentType GetDatabase()
    {
      return new IPContentType(IPContentTypeEnum.DATABASES);
    }

    /// <summary>
    /// Converts the content type to its string representation.
    /// </summary>
    public string AsString()
    {
      string res = EnumUtils.GetXmlEnumName(type);

      if (type == IPContentTypeEnum.OTHER && !string.IsNullOrEmpty(otherType))
      {
        res = otherType;
      }

      return res;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("type: ").Append(EnumUtils.GetXmlEnumName(type));

      if (!string.IsNullOrEmpty(otherType))
      {
        sb.Append("; otherType: ").Append(otherType);
      }

      return sb.ToString();
    }
  }

  /// <summary>
  /// Represents the enumeration of content types for intellectual property (IP) objects.
  /// </summary>
  public enum IPContentTypeEnum
  {
    /// <summary>
    /// Textual works in print format.
    /// </summary>
    [XmlEnum("Textual works – Print")]
    TEXTUAL_WORKS_PRINT,

    /// <summary>
    /// Textual works in digital format.
    /// </summary>
    [XmlEnum("Textual works – Digital")]
    TEXTUAL_WORKS_DIGITAL,

    /// <summary>
    /// Textual works in electronic serials format.
    /// </summary>
    [XmlEnum("Textual works – Electronic Serials")]
    TEXTUAL_WORKS_ELECTRONIC_SERIALS,

    /// <summary>
    /// Digital musical compositions (score-based representations).
    /// </summary>
    [XmlEnum("Digital Musical Composition (score-based representations)")]
    DIGITAL_MUSICAL_COMPOSITION,

    /// <summary>
    /// Photographs in print format.
    /// </summary>
    [XmlEnum("Photographs – Print")]
    PHOTOGRAPHS_PRINT,

    /// <summary>
    /// Photographs in digital format.
    /// </summary>
    [XmlEnum("Photographs – Digital")]
    PHOTOGRAPHS_DIGITAL,

    /// <summary>
    /// Other graphic images in print format.
    /// </summary>
    [XmlEnum("Other Graphic Images – Print")]
    OTHER_GRAPHIC_IMAGES_PRINT,

    /// <summary>
    /// Other graphic images in digital format.
    /// </summary>
    [XmlEnum("Other Graphic Images – Digital")]
    OTHER_GRAPHIC_IMAGES_DIGITAL,

    /// <summary>
    /// Microforms.
    /// </summary>
    [XmlEnum("Microforms")]
    MICROFORMS,

    /// <summary>
    /// Audio on tangible medium (digital or analog).
    /// </summary>
    [XmlEnum("Audio – On Tangible Medium (digital or analog)")]
    AUDIO_ON_TANGIBLE_MEDIUM,

    /// <summary>
    /// Audio in media-independent (digital) format.
    /// </summary>
    [XmlEnum("Audio – Media-independent (digital)")]
    AUDIO_MEDIA_INDEPENDENT,

    /// <summary>
    /// Motion pictures in digital and physical media.
    /// </summary>
    [XmlEnum("Motion Pictures – Digital and Physical Media")]
    MOTION_PICTURES,

    /// <summary>
    /// Video in file-based and physical media.
    /// </summary>
    [XmlEnum("Video – File-based and Physical Media")]
    VIDEO,

    /// <summary>
    /// Software.
    /// </summary>
    [XmlEnum("Software")]
    SOFTWARE,

    /// <summary>
    /// Datasets.
    /// </summary>
    [XmlEnum("Datasets")]
    DATASETS,

    /// <summary>
    /// Dataset.
    /// </summary>
    [XmlEnum("Dataset")]
    DATASET,

    /// <summary>
    /// Geospatial data.
    /// </summary>
    [XmlEnum("Geospatial Data")]
    GEOSPATIAL_DATA,

    /// <summary>
    /// Databases.
    /// </summary>
    [XmlEnum("Databases")]
    DATABASES,

    /// <summary>
    /// Websites.
    /// </summary>
    [XmlEnum("Websites")]
    WEBSITES,

    /// <summary>
    /// Collection.
    /// </summary>
    [XmlEnum("Collection")]
    COLLECTION,

    /// <summary>
    /// Event.
    /// </summary>
    [XmlEnum("Event")]
    EVENT,

    /// <summary>
    /// Interactive resource.
    /// </summary>
    [XmlEnum("Interactive resource")]
    INTERACTIVE_RESOURCE,

    /// <summary>
    /// Physical object.
    /// </summary>
    [XmlEnum("Physical object")]
    PHYSICAL_OBJECT,

    /// <summary>
    /// Service.
    /// </summary>
    [XmlEnum("Service")]
    SERVICE,

    /// <summary>
    /// Mixed content type.
    /// </summary>
    [XmlEnum("Mixed")]
    MIXED,

    /// <summary>
    /// Other content type.
    /// </summary>

    [XmlEnum("Other")]
    OTHER,
  }
}