using System.Xml.Serialization;

public enum CSIPVersion {
  [XmlEnum("2.0.4")]
  V204,

  [XmlEnum("2.1.0")]
  V210,

  [XmlEnum("2.2.0")]
  V220
}