using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Runtime.InteropServices;

/// <summary>
/// Provides utility methods for various operations such as file handling, system checks, and resource management.
/// </summary>
public static class Utils
{
    private static readonly ILogger logger = DefaultLogger.Create("Utils");

    /// <summary>
    /// Determines whether the current operating system is Windows.
    /// </summary>
    /// <returns>True if the operating system is Windows; otherwise, false.</returns>
    public static bool IsWindowsSystem()
    {
        return RuntimeInformation.OSDescription.ToLower().Contains("windows");
    }

    /// <summary>
    /// Generates a random UUID prefixed with a predefined identifier.
    /// </summary>
    /// <returns>A string containing the prefixed UUID.</returns>
    public static string GenerateRandomAndPrefixedUUID()
    {
        return METSEnums.ID_PREFIX + Guid.NewGuid().ToString().ToUpper();
    }

    /// <summary>
    /// Generates a random UUID prefixed with a predefined file identifier.
    /// </summary>
    /// <returns>A string containing the prefixed file UUID.</returns>
    public static string GenerateRandomAndPrefixedFileID()
    {
        return METSEnums.FILE_ID_PREFIX + Guid.NewGuid().ToString().ToUpper();
    }

    /// <summary>
    /// Copies an embedded resource from the classpath to a specified directory with a temporary suffix.
    /// </summary>
    /// <param name="resourceType">The type used to locate the assembly containing the resource.</param>
    /// <param name="dir">The target directory where the resource will be copied.</param>
    /// <param name="resourceTempSuffix">The temporary suffix to append to the resource file name.</param>
    /// <param name="resourcePath">The path of the embedded resource within the assembly.</param>
    /// <returns>The full path of the copied resource file.</returns>
    /// <exception cref="FileNotFoundException">Thrown if the embedded resource is not found.</exception>
    public static string CopyResourceFromClasspathToDir(Type resourceType, string dir, string resourceTempSuffix, string resourcePath)
    {
        string resource = Path.Combine(dir, Path.GetRandomFileName() + resourceTempSuffix);

        Assembly assembly = resourceType.Assembly;
        using (Stream inputStream = assembly.GetManifestResourceStream(resourcePath))
        {
            if (inputStream == null) throw new FileNotFoundException($"Embedded resource not found: {resourcePath}");

            using (FileStream outputStream = File.Create(resource))
            {
                inputStream.CopyTo(outputStream);
            }
        }

        return resource;
    }

    /// <summary>
    /// Deletes the specified directory and its contents.
    /// </summary>
    /// <param name="path">The path of the directory to delete.</param>
    public static void DeleteDirectory(string path)
    {
        if (path == null) return;

        string[] files = Directory.GetFiles(path) ?? new string[0];
        if (files.Length > 0)
        {
            logger.LogDebug("Directory {path} is not empty. Going to delete its content as well.", path);
            foreach (string file in files)
            {
                File.Delete(file);
            }
        }

        Directory.Delete(path);
    }
}