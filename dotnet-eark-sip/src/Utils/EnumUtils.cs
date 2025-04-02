using System.Reflection;
using System.Xml.Serialization;

/// <summary>
/// Provides utility methods for working with enums and their XML attributes.
/// </summary>
public static class EnumUtils
{
    /// <summary>
    /// Retrieves an enum value of type <typeparamref name="T"/> based on its XML attribute name.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="value">The XML attribute name to match.</param>
    /// <returns>The matching enum value of type <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentException">Thrown when no matching enum value is found for the provided XML attribute name.</exception>
    public static T GetEnumFromXmlAttribute<T>(string value) where T : Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            var attribute = field.GetCustomAttribute<XmlEnumAttribute>();
            if (attribute != null && attribute.Name == value)
            {
                return (T)field.GetValue(null);
            }
        }

        throw new ArgumentException($"No matching enum value found for {value}");
    }

    /// <summary>
    /// Retrieves the XML attribute name associated with a given enum value.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="enumValue">The enum value to retrieve the XML attribute name for.</param>
    /// <returns>The XML attribute name if defined; otherwise, the string representation of the enum value.</returns>
    public static string GetXmlEnumName<T>(T enumValue) where T : Enum
    {
        string enumValueString = enumValue.ToString();
        FieldInfo field = typeof(T).GetField(enumValueString);
        var attribute = field.GetCustomAttribute<XmlEnumAttribute>();

        return attribute?.Name ?? enumValueString;
    }
}