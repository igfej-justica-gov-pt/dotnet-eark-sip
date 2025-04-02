using System.Text;
using System.Xml;
using System.Xml.Serialization;
using HeyRed.Mime;
using Mets;
using Microsoft.Extensions.Logging;

/// <summary>
/// Provides utility methods for handling METS (Metadata Encoding and Transmission Standard) operations.
/// </summary>
public static class METSUtils
{
    /// <summary>
    /// Serializes a METS (Metadata Encoding and Transmission Standard) object to an XML file.
    /// </summary>
    /// <param name="mets">The METS object to be serialized.</param>
    /// <param name="tempMETSFile">The file path where the serialized XML will be saved.</param>
    /// <param name="mainMets">
    /// A boolean indicating whether the METS object is the main METS file. 
    /// If true, the schema locations will be set for the main METS file; otherwise, 
    /// relative paths will be used for schema locations.
    /// </param>
    /// <returns>The file path of the serialized METS XML file.</returns>
    /// <remarks>
    /// This method uses an <see cref="XmlSerializer"/> to serialize the METS object into an XML file. 
    /// It also applies specific namespaces and schema locations based on whether the METS object 
    /// is the main METS file or not. The resulting XML file is indented for readability.
    /// </remarks>
    public static string MarshallMETS(Mets.Mets mets, string tempMETSFile, bool mainMets)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(Mets.Mets));
        XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        ns.Add("csip", "https://DILCIS.eu/XML/METS/CSIPExtensionMETS");
        ns.Add("sip", "https://DILCIS.eu/XML/METS/SIPExtensionMETS");
        ns.Add("xlink", "http://www.w3.org/1999/xlink");

        if (mainMets)
        {
            mets.SchemaLocation = "http://www.loc.gov/METS/ schemas/" + IPConstants.SCHEMA_METS_FILENAME_WITH_VERSION
              + " http://www.w3.org/1999/xlink schemas/" + IPConstants.SCHEMA_XLINK_FILENAME
              + " https://DILCIS.eu/XML/METS/CSIPExtensionMETS schemas/" + IPConstants.SCHEMA_EARK_CSIP_FILENAME
              + " https://DILCIS.eu/XML/METS/SIPExtensionMETS schemas/" + IPConstants.SCHEMA_EARK_SIP_FILENAME;
        }
        else
        {
            mets.SchemaLocation = "http://www.loc.gov/METS/ ../../schemas/" + IPConstants.SCHEMA_METS_FILENAME_WITH_VERSION
                + " http://www.w3.org/1999/xlink ../../schemas/" + IPConstants.SCHEMA_XLINK_FILENAME
                + " https://DILCIS.eu/XML/METS/CSIPExtensionMETS ../../schemas/" + IPConstants.SCHEMA_EARK_CSIP_FILENAME
                + " https://DILCIS.eu/XML/METS/SIPExtensionMETS ../../schemas/" + IPConstants.SCHEMA_EARK_SIP_FILENAME;
        }

        using (FileStream fileStream = new FileStream(tempMETSFile, FileMode.Create))
        using (XmlWriter writer = XmlWriter.Create(fileStream, settings))
        {
            serializer.Serialize(writer, mets, ns);
        }

        return tempMETSFile;
    }

    /// <summary>
    /// Adds the main METS file to a ZIP archive.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="metsWrapper">The wrapper containing the METS object.</param>
    /// <param name="metsPath">The path to the METS file.</param>
    /// <param name="buildDir">The directory where the temporary METS file is created.</param>
    public static void AddMainMETSToZip(Dictionary<string, IZipEntryInfo> zipEntries, MetsWrapper metsWrapper, string metsPath, string buildDir)
    {
        try
        {
            AddMETSToZip(zipEntries, metsWrapper, metsPath, buildDir, true, null);
        }
        catch (Exception e)
        {
            if (e is XmlException || e is IOException)
            {
                throw new IPException(e.Message, e);
            }
            else
            {
                throw e;
            }
        }
    }

    /// <summary>
    /// Adds the main METS file to a ZIP archive using the default METS file path.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="metsWrapper">The wrapper containing the METS object.</param>
    /// <param name="buildDir">The directory where the temporary METS file is created.</param>
    public static void AddMainMETSToZip(Dictionary<string, IZipEntryInfo> zipEntries, MetsWrapper metsWrapper, string buildDir)
    {
        AddMainMETSToZip(zipEntries, metsWrapper, IPConstants.METS_FILE, buildDir);
    }

    /// <summary>
    /// Adds a METS file to a ZIP archive.
    /// </summary>
    /// <param name="zipEntries">The dictionary of ZIP entries.</param>
    /// <param name="metsWrapper">The wrapper containing the METS object.</param>
    /// <param name="metsPath">The path to the METS file.</param>
    /// <param name="buildDir">The directory where the temporary METS file is created.</param>
    /// <param name="mainMets">Indicates whether the METS file is the main METS file.</param>
    /// <param name="fileType">The type of the file being added, if applicable.</param>
    public static void AddMETSToZip(
      Dictionary<string,
      IZipEntryInfo> zipEntries,
      MetsWrapper metsWrapper,
      string metsPath,
      string buildDir,
      bool mainMets,
      FileType? fileType
    )
    {
        string temp = Path.Combine(buildDir, IPConstants.METS_FILE_NAME + IPConstants.METS_FILE_EXTENSION);
        File.Create(temp).Dispose();
        ZIPUtils.AddMETSFileToZip(zipEntries, temp, metsPath, metsWrapper.Mets, mainMets, fileType);
    }

    /// <summary>
    /// Creates a file location object for the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the file for which the location object is created.</param>
    /// <returns>A <see cref="FileTypeFLocat"/> object representing the file location.</returns>
    public static FileTypeFLocat CreateFileLocation(string filePath)
    {
        FileTypeFLocat fileLocation = new FileTypeFLocat
        {
            Type = IPConstants.METS_TYPE_SIMPLE,
            Loctype = ILocationLoctype.URL,
            Href = EncodeHref(filePath)
        };

        return fileLocation;
    }

    /// <summary>
    /// Creates a shallow file location object for the specified file path.
    /// </summary>
    /// <param name="filePath">The path to the file for which the location object is created.</param>
    /// <returns>A <see cref="FileTypeFLocat"/> object representing the shallow file location.</returns>
    public static FileTypeFLocat CreateShallowFileLocation(string filePath)
    {
        FileTypeFLocat fileLocation = new FileTypeFLocat
        {
            Type = IPConstants.METS_TYPE_SIMPLE,
            Loctype = ILocationLoctype.URL,
            Href = filePath
        };

        return fileLocation;
    }

    /// <summary>
    /// Sets basic information for a file, including MIME type, creation date, and size.
    /// </summary>
    /// <param name="filePath">The path to the file.</param>
    /// <param name="mdRef">The metadata reference object to update with file information.</param>
    /// <returns>The updated <see cref="MdSecTypeMdRef"/> object with file information.</returns>
    public static MdSecTypeMdRef SetFileBasicInformation(string filePath, MdSecTypeMdRef mdRef)
    {
        try
        {
            mdRef.Mimetype = GetFileMimetype(filePath);
        }
        catch (IOException e)
        {
            throw new IPException("Error probing file content: " + filePath, e);
        }

        try
        {
            mdRef.Created = DateTime.Now;
            mdRef.CreatedSpecified = true;
        }
        catch (InvalidOperationException e)
        {
            throw new IPException("Error getting current date", e);
        }

        try
        {
            FileInfo fileInfo = new FileInfo(filePath);
            mdRef.Size = fileInfo.Length;
            mdRef.SizeSpecified = true;
        }
        catch (IOException e)
        {
            throw new IPException("Error getting file size: " + filePath, e);
        }

        return mdRef;
    }

    /// <summary>
    /// Sets basic information for a file, including MIME type, creation date, and size, with logging.
    /// </summary>
    /// <param name="logger">The logger used for debugging information.</param>
    /// <param name="filePath">The path to the file.</param>
    /// <param name="fileType">The file type object to update with file information.</param>
    public static void SetFileBasicInformation(ILogger logger, string filePath, FileType fileType)
    {
        try
        {
            logger.LogDebug("Setting mimetype for {file}", filePath);
            fileType.Mimetype = GetFileMimetype(filePath);
            logger.LogDebug("Done setting mimetype for {file}", filePath);
        }
        catch (IOException e)
        {
            throw new IPException("Error probing content type: " + filePath, e);
        }

        try
        {
            fileType.Created = DateTime.Now;
            fileType.CreatedSpecified = true;
        }
        catch (InvalidOperationException e)
        {
            throw new IPException("Error getting current date: " + filePath, e);
        }

        try
        {
            logger.LogDebug("Setting file size for {file}", filePath);
            fileType.Size = new FileInfo(filePath).Length;
            fileType.SizeSpecified = true;
            logger.LogDebug("Done setting file size for {file}", filePath);
        }
        catch (IOException e)
        {
            throw new IPException("Error getting file size: " + filePath, e);
        }
    }

    private static string GetFileMimetype(string path)
    {
        string mimetype = MimeTypesMap.GetMimeType(path);
        if (mimetype == null || !IanaMediaTypes.List.Contains(mimetype))
        {
            mimetype = "application/octet-stream";
        }

        return mimetype;
    }

    /// <summary>
    /// Encodes the given HREF value by escaping special characters if encoding is enabled.
    /// </summary>
    /// <param name="value">The HREF value to encode.</param>
    /// <returns>The encoded HREF value.</returns>
    public static string EncodeHref(string value)
    {
        if (IPConstants.METS_ENCODE_AND_DECODE_HREF)
        {
            value = EscapeSpecialCharacters(value);
        }

        return value;
    }

    /// <summary>
    /// Escapes special characters in the input string to make it safe for use in URLs or XML.
    /// </summary>
    /// <param name="input">The input string containing special characters.</param>
    /// <returns>A string with special characters escaped.</returns>
    public static string EscapeSpecialCharacters(string input)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in input.ToCharArray())
        {
            if (IsSafeChar(c))
            {
                result.Append(c);
            }
            else
            {
                result.Append(EncodeUnsafeChar(c));
            }
        }

        return result.ToString();
    }

    private static bool IsSafeChar(char c)
    {
        return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || ":/$-_.!*'(),".IndexOf(c) >= 0;
    }

    private static string EncodeUnsafeChar(char c)
    {
        string result = c.ToString();

        try
        {
            result = Uri.EscapeDataString(result);
        }
        catch
        {
            // do nothing & return original value
        }

        return result;
    }
}