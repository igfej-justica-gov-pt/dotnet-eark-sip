using System.Text;

public class IanaMediaTypes {
  private static HashSet<string> list = null;

  public static HashSet<string> List {
    get {
      if (list == null) list = LoadIanaMediaTypes();
      return list;
    }
  }

  private IanaMediaTypes() {}

  public static HashSet<string> LoadIanaMediaTypes() {
    string filePath = Constants.IANA_MEDIA_TYPES_PATH;

    if (!File.Exists(filePath)) {
      throw new FileNotFoundException($"File not found: {filePath}");
    }

    return new HashSet<string>(File.ReadLines(filePath, Encoding.UTF8));
  }
}