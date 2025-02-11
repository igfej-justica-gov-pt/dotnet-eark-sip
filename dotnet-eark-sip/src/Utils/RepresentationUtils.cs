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

  public static void IncludeInRepresentation(string directory, IPRepresentation representation, List<string> relativeFolders) {
    if (Directory.Exists(directory)) {
      List<string> newRelativeFolders = new List<string>(relativeFolders);
      newRelativeFolders.AddRange(directory.Split(Path.DirectorySeparatorChar));

      IEnumerable<string> subDirectories = Directory.GetDirectories(directory);
      foreach (string subDirectory in subDirectories) {
        IncludeInRepresentation(subDirectory, representation, newRelativeFolders);
      }

      IEnumerable<string> files = Directory.GetFiles(directory);
      foreach (string file in files) {
        AddToRepresentation(file, representation, newRelativeFolders);
      }
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