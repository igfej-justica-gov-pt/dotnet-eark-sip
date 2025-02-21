using System.Xml.Serialization;

public enum WriteStrategyEnum {
  [XmlEnum("Zip")]
  ZIP,

  [XmlEnum("Folder")]
  FOLDER
}