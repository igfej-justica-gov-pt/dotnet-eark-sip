using System.Text;
using System.Xml;
using System.Xml.Serialization;
using HeyRed.Mime;
using Mets;

public static class METSUtils {
  public static string MarshallMETS(Mets.Mets mets, string tempMETSFile, bool mainMets) {
    XmlSerializer serializer = new XmlSerializer(typeof(Mets.Mets));
    XmlWriterSettings settings = new XmlWriterSettings { Indent = true };

    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
    ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
    ns.Add("csip", "https://DILCIS.eu/XML/METS/CSIPExtensionMETS");
    ns.Add("sip", "https://DILCIS.eu/XML/METS/SIPExtensionMETS");
    ns.Add("xlink", "http://www.w3.org/1999/xlink");

    if (mainMets) {
      mets.SchemaLocation = "http://www.loc.gov/METS/ schemas/" + IPConstants.SCHEMA_METS_FILENAME_WITH_VERSION
        + " http://www.w3.org/1999/xlink schemas/" + IPConstants.SCHEMA_XLINK_FILENAME
        + " https://DILCIS.eu/XML/METS/CSIPExtensionMETS schemas/" + IPConstants.SCHEMA_EARK_CSIP_FILENAME
        + " https://DILCIS.eu/XML/METS/SIPExtensionMETS schemas/" + IPConstants.SCHEMA_EARK_SIP_FILENAME;
    } else {
      mets.SchemaLocation = "http://www.loc.gov/METS/ ../../schemas/" + IPConstants.SCHEMA_METS_FILENAME_WITH_VERSION
          + " http://www.w3.org/1999/xlink ../../schemas/" + IPConstants.SCHEMA_XLINK_FILENAME
          + " https://DILCIS.eu/XML/METS/CSIPExtensionMETS ../../schemas/" + IPConstants.SCHEMA_EARK_CSIP_FILENAME
          + " https://DILCIS.eu/XML/METS/SIPExtensionMETS ../../schemas/" + IPConstants.SCHEMA_EARK_SIP_FILENAME;
    }

    using (FileStream fileStream = new FileStream(tempMETSFile, FileMode.Create))
    using (XmlWriter writer = XmlWriter.Create(fileStream, settings)) {
      serializer.Serialize(writer, mets, ns);
    }

    return tempMETSFile;
  }

  public static void AddMainMETSToZip(Dictionary<string, IZipEntryInfo> zipEntries, MetsWrapper metsWrapper, string metsPath, string buildDir) {
    try {
      AddMETSToZip(zipEntries, metsWrapper, metsPath, buildDir, true, null);
    } catch (Exception e) {
      if (e is XmlException || e is IOException) {
        throw new IPException(e.Message, e);
      } else {
        throw e;
      }
    }
  }

  public static void AddMainMETSToZip(Dictionary<string, IZipEntryInfo> zipEntries, MetsWrapper metsWrapper, string buildDir) {
    AddMainMETSToZip(zipEntries, metsWrapper, IPConstants.METS_FILE, buildDir);
  }

  public static void AddMETSToZip(
    Dictionary<string,
    IZipEntryInfo> zipEntries,
    MetsWrapper metsWrapper,
    string metsPath,
    string buildDir,
    bool mainMets,
    FileType fileType
  ) {
    string temp = Path.Combine(buildDir, IPConstants.METS_FILE_NAME + IPConstants.METS_FILE_EXTENSION);
    File.Create(temp).Dispose();
    ZIPUtils.AddMETSFileToZip(zipEntries, temp, metsPath, metsWrapper.Mets, mainMets, fileType);
  }

  public static FileTypeFLocat CreateFileLocation(string filePath) {
    FileTypeFLocat fileLocation = new FileTypeFLocat
    {
      Type = IPConstants.METS_TYPE_SIMPLE,
      Loctype = ILocationLoctype.URL,
      Href = EncodeHref(filePath)
    };

    return fileLocation;
  }

  public static FileTypeFLocat CreateShallowFileLocation(string filePath) {
    FileTypeFLocat fileLocation = new FileTypeFLocat
    {
      Type = IPConstants.METS_TYPE_SIMPLE,
      Loctype = ILocationLoctype.URL,
      Href = filePath
    };

    return fileLocation;
  }

  public static MdSecTypeMdRef SetFileBasicInformation(string filePath, MdSecTypeMdRef mdRef) {
    try {
      mdRef.Mimetype = GetFileMimetype(filePath);
    } catch (IOException e) {
      throw new IPException("Error probing file content: " + filePath, e);
    }

    try {
      mdRef.Created = DateTime.Now;
      mdRef.CreatedSpecified = true;
    } catch (InvalidOperationException e) {
      throw new IPException("Error getting current date", e);
    }

    try {
      FileInfo fileInfo = new FileInfo(filePath);
      mdRef.Size = fileInfo.Length;
      mdRef.SizeSpecified = true;
    } catch (IOException e) {
      throw new IPException("Error getting file size: " + filePath, e);
    }

    return mdRef;
  }

  // TODO: Add logger
  public static void SetFileBasicInformation(string filePath, FileType fileType) {
    try {
      // logger.Debug("Setting mimetype " + filePath);
      fileType.Mimetype = GetFileMimetype(filePath);
      // logger.Debug("Done setting mimetype");
    } catch (IOException e) {
      throw new IPException("Error probing content type: " + filePath, e);
    }

    try {
      fileType.Created = DateTime.Now;
      fileType.CreatedSpecified = true;
    } catch (InvalidOperationException e) {
      throw new IPException("Error getting current date: " + filePath, e);
    }

    try {
      // logger.Debug("Setting file size " + filePath);
      FileInfo fileInfo = new FileInfo(filePath);
      fileType.Size = fileInfo.Length;
      fileType.SizeSpecified = true;
      // logger.Debug("Done setting file size");
    } catch (IOException e) {
      throw new IPException("Error getting file size: " + filePath, e);
    }
  }

  private static string GetFileMimetype(string path) {
    string mimetype = MimeTypesMap.GetMimeType(path);
    if (mimetype == null || !IanaMediaTypes.List.Contains(mimetype)) {
      mimetype = "application/octet-stream";
    }

    return mimetype;
  }

  public static string EncodeHref(string value) {
    if (IPConstants.METS_ENCODE_AND_DECODE_HREF) {
      value = EscapeSpecialCharacters(value);
    }

    return value;
  }

  public static string EscapeSpecialCharacters(string input) {
    StringBuilder result = new StringBuilder();
    foreach (char c in input.ToCharArray()) {
      if (IsSafeChar(c)) {
        result.Append(c);
      } else {
        result.Append(EncodeUnsafeChar(c));
      }
    }

    return result.ToString();
  }

  private static bool IsSafeChar(char c) {
    return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9') || ":/$-_.!*'(),".IndexOf(c) >= 0;
  }

  private static string EncodeUnsafeChar(char c) {
    string result = c.ToString();

    try {
      result = Uri.EscapeDataString(result);
    } catch {
      // do nothing & return original value
    }

    return result;
  }
}