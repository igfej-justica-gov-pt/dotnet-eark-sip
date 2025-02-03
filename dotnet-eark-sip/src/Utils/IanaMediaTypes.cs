using System.Reflection;
using System.Text;

public static class IanaMediaTypes {
  private static HashSet<string> list = null;

  public static HashSet<string> List {
    get {
      if (list == null) list = LoadIanaMediaTypes();
      return list;
    }
  }

  public static HashSet<string> LoadIanaMediaTypes() {
    var assembly = Assembly.GetExecutingAssembly();
    string resourceName = "dotnet_eark_sip.src.Resources.ControlledVocabularies.IANA_MEDIA_TYPES.txt";

    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
    using (StreamReader reader = new(stream, Encoding.UTF8)) {
      var lines = new List<string>();
      while (!reader.EndOfStream) {
        lines.Add(reader.ReadLine());
      }

      return new HashSet<string>(lines);
    }
  }
}