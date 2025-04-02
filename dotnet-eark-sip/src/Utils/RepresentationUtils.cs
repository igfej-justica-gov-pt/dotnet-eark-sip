using IP;

/// <summary>
/// Provides utility methods for handling representations in the IP system.
/// </summary>
public class RepresentationUtils
{
    /// <summary>
    /// Adds the specified paths to the given representation.
    /// </summary>
    /// <param name="paths">A list of file or directory paths to add.</param>
    /// <param name="representation">The representation to which the paths will be added.</param>
    public static void AddToRepresentation(List<string> paths, IPRepresentation representation)
    {
        foreach (string item in paths)
        {
            AddToRepresentation(item, representation, new());
        }
    }

    /// <summary>
    /// Includes the contents of the specified directory in the given representation.
    /// </summary>
    /// <param name="directory">The directory whose contents will be included.</param>
    /// <param name="representation">The representation to which the directory contents will be added.</param>
    public static void IncludeInRepresentation(string directory, IPRepresentation representation)
    {
        IncludeInRepresentation(directory, representation, new());
    }

    /// <summary>
    /// Includes the contents of the specified directory in the given representation.
    /// </summary>
    /// <param name="path">The path to include the contents from.</param>
    /// <param name="representation">The representation to which the contents will be added.</param>
    /// <param name="relativeFolders">A list of relative folder names to maintain the directory structure.</param>
    public static void IncludeInRepresentation(string path, IPRepresentation representation, List<string> relativeFolders)
    {
        if (Directory.Exists(path))
        {
            IEnumerable<string> subDirectories = Directory.GetDirectories(path);
            foreach (string subDirectory in subDirectories)
            {
                string directoryName = subDirectory.Split(Path.DirectorySeparatorChar).Last();
                List<string> newRelativeFolders = new List<string>(relativeFolders) { directoryName };
                IncludeInRepresentation(subDirectory, representation, newRelativeFolders);
            }

            IEnumerable<string> files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                AddToRepresentation(file, representation, relativeFolders);
            }
        }
        else if (File.Exists(path))
        {
            AddToRepresentation(path, representation, relativeFolders);
        }
    }

    /// <summary>
    /// Adds the specified path to the given representation, maintaining the relative folder structure.
    /// </summary>
    /// <param name="path">The file or directory path to add.</param>
    /// <param name="representation">The representation to which the path will be added.</param>
    /// <param name="relativeFolders">A list of relative folder names to maintain the directory structure.</param>
    public static void AddToRepresentation(string path, IPRepresentation representation, List<string> relativeFolders)
    {
        if (File.Exists(path))
        {
            representation.AddFile(new IPFile(path, relativeFolders));
        }
        else if (Directory.Exists(path))
        {
            IncludeInRepresentation(path, representation, relativeFolders);
        }
    }
}