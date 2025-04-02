using System.Xml.Serialization;

/// <summary>
/// Represents the metadata status.
/// </summary>
public enum MetadataStatus
{
    /// <summary>
    /// Represents the current metadata status.
    /// </summary>
    [XmlEnum("CURRENT")]
    CURRENT,

    /// <summary>
    /// Represents the superseded metadata status.
    /// </summary>
    [XmlEnum("SUPERSEDED")]
    SUPERSEDED
}