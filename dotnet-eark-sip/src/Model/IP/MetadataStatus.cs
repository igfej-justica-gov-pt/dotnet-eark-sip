using System.Xml.Serialization;

public enum MetadataStatus {
  [XmlEnum("CURRENT")]
  CURRENT,

  [XmlEnum("SUPERSEDED")]
  SUPERSEDED
}