using System.Net;
using IP;
using Mets;

public class SIPBuilderUtils {
  public static string GetOrGenerateID(string sipId) {
    string id = sipId;

    if (string.IsNullOrEmpty(id)) {
      id = Utils.GenerateRandomAndPrefixedUUID();
    } else {
      id = WebUtility.UrlEncode(sipId).Replace("*", "%2A");
    }

    return id;
  }

  public static void AddDocumentationToSIP(SIP sip, List<string> documentation) {
    foreach (string doc in documentation) {
      if (Directory.Exists(doc)) {
        IEnumerable<string> filesInDirectory = Directory.EnumerateFiles(doc, "*", SearchOption.AllDirectories);
        foreach (string docFile in filesInDirectory) {
          sip.AddDocumentation(new IPFile(docFile));
        }
      } else {
        sip.AddDocumentation(new IPFile(doc));
      }
    }
  }

  public static void AddRepresentationGroupsToSIP(SIP sip, List<RepresentationGroup> groups, bool targetOnly) {
    if (groups == null) return;

    foreach (RepresentationGroup group in groups) {
      AddRepresentationToSIP(sip, group, targetOnly);
    }
  }

  private static void AddRepresentationToSIP(SIP sip, RepresentationGroup group, bool targetOnly) {
    string id = group.Representation.RepresentationId;
    id ??= "rep1";

    IPRepresentation representation = new(id);
    sip.AddRepresentation(representation);

    if (group.Representation.RepresentationType != null) {
      IPContentType ipContentType = GetIPContentType(group.Representation.RepresentationType);
      representation.SetContentType(ipContentType);
    }

    foreach (string path in group.Representation.RepresentationData) {
      AddFileToRepresentation(representation, targetOnly, path, new List<string>());
    }
  }

  public static void AddRepresentationGroupsToErmsSIP(SIP sip, List<RepresentationGroup> groups, bool targetOnly) {
    AddRepresentationGroupsToSIP(sip, groups, targetOnly);
  }

  private static void AddFileToRepresentation(IPRepresentation representation, bool targetOnly, string path, List<string> relativePath) {
    if (Directory.Exists(path)) {
      List<string> newRelativePath = new(relativePath);

      if (!targetOnly) newRelativePath.Add(path);

      string[] files = Directory.GetFiles(path);
      if (files != null && files.Length > 0) {
        foreach (string file in files) {
          AddFileToRepresentation(representation, false, file, newRelativePath);
        }
      }
    } else {
      if (relativePath.Count > 0) {
        representation.AddFile(path, relativePath);
      } else {
        representation.AddFile(new IPFile(path));
      }
    }
  }

  private static IPContentType GetIPContentType(string representationType) {
    return IPContentType.GetMIXED();
  }

  public static void AddMetadataGroupsToSIP(SIP sip, List<MetadataGroup> groups) {
    if (groups != null) {
      foreach (MetadataGroup group in groups) {
        AddMetadataToSIP(sip, group);
      }
    }
  }

  private static void AddMetadataToSIP(SIP sip, MetadataGroup group) {
    string metadataFile = group.Metadata.MetadataFile;
    string metadataType = group.Metadata.MetadataType;
    string metadataVersion = group.Metadata.MetadataVersion;

    MetadataType metadataTypeEnum;
    string version = metadataVersion;

    if (metadataType == null && metadataVersion == null) {
      metadataTypeEnum = GetMetadataTypeFromMetadataFile(metadataFile);
      version = GetMetadataVersionFromMetadataFile(metadataFile);
    } else if (metadataVersion != null && metadataType == null) {
      metadataTypeEnum = GetMetadataTypeFromMetadataFile(metadataFile);
    } else if (metadataVersion == null) {
      metadataTypeEnum = new MetadataType(metadataType);
      version = GetMetadataVersionFromMetadataFile(metadataFile);
    } else {
      metadataTypeEnum = new MetadataType(metadataType);
    }

    IPDescriptiveMetadata descriptiveMetadata = new IPDescriptiveMetadata(new IPFile(metadataFile), metadataTypeEnum, version);
    sip.AddDescriptiveMetadata(descriptiveMetadata);

    string metadataSchema = group.Metadata.MetadataSchema;
    if (metadataSchema != null) sip.AddSchema(new IPFile(metadataSchema));
  }

  private static MetadataType GetMetadataTypeFromMetadataFile(string metadataFile) {
    string filename = Path.GetFileNameWithoutExtension(metadataFile);
    MetadataType metadataType = new(filename);

    if (metadataType._GetType() == IMetadataMdtype.OTHER) {
      string[] splitFilename = filename.Split('_');
      if (splitFilename.Length == 2) metadataType = new MetadataType(splitFilename[0]);
    }

    return metadataType;
  }

  private static string GetMetadataVersionFromMetadataFile(string metadataFile) {
    string filename = Path.GetFileNameWithoutExtension(metadataFile);
    string[] splitFilename = filename.Split('_');
    string metadataVersion = null;

    if (splitFilename.Length == 2) metadataVersion = splitFilename[1];
    return metadataVersion;
  }

  public static IWriteStrategy GetWriteStrategy(WriteStrategyEnum writeStrategyEnum, string buildPath) {
    switch (writeStrategyEnum.WriteStrategy) {
      case "Zip":
        ZipWriteStrategyFactory zipWriteStrategyFactory = new();
        return zipWriteStrategyFactory.Create(buildPath);

      case "Folder":
        FolderWriteStrategyFactory folderWriteStrategyFactory = new();
        return folderWriteStrategyFactory.Create(buildPath);

      default:
        return null;
    }
  }

  private SIPBuilderUtils() {}
}