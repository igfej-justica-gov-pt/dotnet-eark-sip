using System.Reflection;
using System.Text;

/// <summary>
/// Provides functionality to load and access IANA media types.
/// </summary>
public static class IanaMediaTypes
{
  private static HashSet<string>? list = null;

  /// <summary>
  /// Gets the list of IANA media types, loading them if not already loaded.
  /// </summary>
  public static HashSet<string> List
  {
    get
    {
      if (list == null) list = LoadIanaMediaTypes();
      return list;
    }
  }

  /// <summary>
  /// Loads the IANA media types from the embedded resource file.
  /// </summary>
  /// <returns>A HashSet containing the IANA media types.</returns>
  public static HashSet<string> LoadIanaMediaTypes()
  {
    var assembly = Assembly.GetExecutingAssembly();
    string resourceName = "dotnet_eark_sip.src.Resources.ControlledVocabularies.IANA_MEDIA_TYPES.txt";

    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
    using (StreamReader reader = new(stream, Encoding.UTF8))
    {
      var lines = new List<string>();
      while (!reader.EndOfStream)
      {
        lines.Add(reader.ReadLine());
      }

      return new HashSet<string>(lines);
    }
  }
}