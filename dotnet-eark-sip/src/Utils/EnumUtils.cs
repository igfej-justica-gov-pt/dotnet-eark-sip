using System.Reflection;
using System.Xml.Serialization;

public static class EnumUtils {
  public static T GetEnumFromXmlAttribute<T>(string value) where T : Enum {
    foreach (var field in typeof(T).GetFields()) {
      var attribute = field.GetCustomAttribute<XmlEnumAttribute>();
      if (attribute != null && attribute.Name == value) {
        return (T)field.GetValue(null);
      }
    }

    throw new ArgumentException($"No matching enum value found for {value}");
  }

  public static string GetXmlEnumName<T>(T enumValue) where T : Enum {
    string enumValueString = enumValue.ToString();
    FieldInfo field = typeof(T).GetField(enumValueString);
    var attribute = field.GetCustomAttribute<XmlEnumAttribute>();

    return attribute?.Name ?? enumValueString;
  }
}