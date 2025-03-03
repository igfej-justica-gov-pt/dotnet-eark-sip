using IP;

public class RepresentationUtils {
  public static void AddToRepresentation(List<string> paths, IPRepresentation representation) {
    foreach (string item in paths) {
      AddToRepresentation(item, representation, new());
    }
  }

  public static void IncludeInRepresentation(string directory, IPRepresentation representation) {
    IncludeInRepresentation(directory, representation, new());
  }

  public static void IncludeInRepresentation(string path, IPRepresentation representation, List<string> relativeFolders) {
    if (Directory.Exists(path)) {
      IEnumerable<string> subDirectories = Directory.GetDirectories(path);
      foreach (string subDirectory in subDirectories) {
        string directoryName = subDirectory.Split(Path.DirectorySeparatorChar).Last();
        List<string> newRelativeFolders = new List<string>(relativeFolders) { directoryName };
        IncludeInRepresentation(subDirectory, representation, newRelativeFolders);
      }

      IEnumerable<string> files = Directory.GetFiles(path);
      foreach (string file in files) {
        AddToRepresentation(file, representation, relativeFolders);
      }
    } else if (File.Exists(path)) {
      AddToRepresentation(path, representation, relativeFolders);
    }
  }

  public static void AddToRepresentation(string path, IPRepresentation representation, List<string> relativeFolders) {
    if (File.Exists(path)) {
      representation.AddFile(new IPFile(path, relativeFolders));
    } else if (Directory.Exists(path)) {
      IncludeInRepresentation(path, representation, relativeFolders);
    }
  }
}