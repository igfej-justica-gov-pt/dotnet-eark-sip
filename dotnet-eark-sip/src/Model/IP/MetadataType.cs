using System.Text;
using Mets;

namespace IP
{
  /// <summary>
  /// Represents metadata with a specific type and optional other type information.
  /// </summary>
  [Serializable]
  public class MetadataType
  {
    private readonly IMetadataMdtype type;
    private string otherType;

    /// <summary>
    /// Initializes a new instance of the <see cref="MetadataType"/> class with the specified type and optional other type information.
    /// </summary>
    /// <param name="type">The metadata type.</param>
    /// <param name="otherType">The optional other type information.</param>
    public MetadataType(string type, string? otherType = null)
    {
      try
      {
        this.type = EnumUtils.GetEnumFromXmlAttribute<IMetadataMdtype>(type);
        this.otherType = otherType ?? "";
      }
      catch
      {
        this.type = IMetadataMdtype.OTHER;
        this.otherType = otherType ?? "";
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MetadataType"/> class with the specified type and optional other type information.
    /// </summary>
    /// <param name="type">The metadata type.</param>
    /// <param name="otherType">The optional other type information.</param>
    public MetadataType(IMetadataMdtype type, string? otherType = null)
    {
      this.type = type;
      this.otherType = otherType ?? "";
    }

    public IMetadataMdtype _GetType()
    {
      return type;
    }

    public string GetOtherType()
    {
      return otherType;
    }

    public MetadataType SetOtherType(string otherType)
    {
      this.otherType = otherType;
      return this;
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("type: ").Append(EnumUtils.GetXmlEnumName(type));
      if (string.IsNullOrEmpty(otherType))
      {
        sb.Append("; othertype: ").Append(otherType);
      }

      return sb.ToString();
    }

    public override int GetHashCode()
    {
      int prime = 31;
      int result = 1;

      result = prime * result + ((otherType == null) ? 0 : otherType.GetHashCode());
      result = prime * result + type.GetHashCode();

      return result;
    }

    public override bool Equals(object obj)
    {
      if (this == obj) return true;
      if (obj == null) return false;
      if (!(obj is MetadataType other)) return false;

      return this.type == other._GetType() && this.otherType.Equals(other.GetOtherType());
    }
  }
}