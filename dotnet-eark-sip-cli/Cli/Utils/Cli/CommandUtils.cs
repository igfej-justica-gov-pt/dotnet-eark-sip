public static class CommandUtils {
  public static bool ValidateDocumentationPaths(IEnumerable<string>? documentationPaths) {
    if (documentationPaths != null && documentationPaths.Any()) {
      return documentationPaths.All(File.Exists);
    }

    return true;
  }

  public static bool ValidateRepresentationDataPaths(IEnumerable<Representation>? representationGroups) {
    if (representationGroups != null && representationGroups.Any()) {
      bool valid = true;
      foreach (Representation group in representationGroups) {
        valid &= group.RepresentationData.All(File.Exists);
      }

      return valid;
    }

    return true;
  }

  public static bool ValidateMetadataPaths(IEnumerable<Metadata>? metadataGroups) {
    if (metadataGroups != null && metadataGroups.Any()) {
      return metadataGroups.All(metadata => File.Exists(metadata.MetadataFile));
    }

    return true;
  }

  public static bool ValidateMetadataSchemaPaths(IEnumerable<Metadata>? metadataGroups) {
    if (metadataGroups != null && metadataGroups.Any()) {
      return metadataGroups.All((metadata) => {
        string? schemaPath = metadata.MetadataSchema;
        return schemaPath == null || File.Exists(schemaPath);
      });
    }

    return true;
  }
}