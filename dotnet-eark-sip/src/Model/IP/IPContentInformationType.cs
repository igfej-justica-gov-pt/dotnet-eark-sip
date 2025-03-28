using System.Text;
using Xml.Mets.CsipExtensionMets;

namespace IP
{
  /// <summary>
  /// Represents the content information type for an IP, including its type and optional other type.
  /// </summary>
  [Serializable]
  public class IPContentInformationType
  {
    private readonly Contentinformationtype type;
    private string otherType;

    /// <summary>
    /// Initializes a new instance of the <see cref="IPContentInformationType"/> class with the specified type and optional other type.
    /// </summary>
    /// <param name="type">The content information type.</param>
    /// <param name="otherType">The optional other type.</param>
    public IPContentInformationType(string type, string? otherType = null)
    {
      try
      {
        this.type = EnumUtils.GetEnumFromXmlAttribute<Contentinformationtype>(type);
        this.otherType = otherType ?? "";
      }
      catch
      {
        this.type = Contentinformationtype.OTHER;
        this.otherType = type;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPContentInformationType"/> class with the specified type and optional other type.
    /// </summary>
    /// <param name="type">The content information type.</param>
    /// <param name="otherType">The optional other type.</param>
    public IPContentInformationType(Contentinformationtype type, string? otherType = null)
    {
      this.type = type;
      this.otherType = otherType ?? "";
    }

    public Contentinformationtype GetContentInformationType()
    {
      return type;
    }

    public string GetOtherType()
    {
      return otherType;
    }

    public IPContentInformationType SetOtherType(string otherType)
    {
      this.otherType = otherType;
      return this;
    }

    /// <summary>
    /// Determines whether the content information type is OTHER and the other type is defined.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the content information type is OTHER and the other type is not null or empty; otherwise, <c>false</c>.
    /// </returns>
    public bool IsOtherAndOtherTypeIsDefined()
    {
      return type == Contentinformationtype.OTHER && !string.IsNullOrEmpty(otherType);
    }

    /// <summary>
    /// Gets an instance of <see cref="IPContentInformationType"/> with the type set to MIXED.
    /// </summary>
    /// <returns>An instance of <see cref="IPContentInformationType"/> with the type MIXED.</returns>
    public static IPContentInformationType GetMIXED()
    {
      return new IPContentInformationType(Contentinformationtype.MIXED);
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
}