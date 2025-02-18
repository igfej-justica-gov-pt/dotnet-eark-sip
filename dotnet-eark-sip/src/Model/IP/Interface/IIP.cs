using IP;
using IPEnums;

public interface IIP {
  IIP SetId(string id);
  string GetId();

  IIP SetIds(List<string> ids);
  List<string> GetIds();

  IIP SetProfile(string profile);
  string GetProfile();

  IIP SetType(IPType type);
  string _GetType();

  IIP SetContentType(IPContentType contentType);
  IPContentType GetContentType();

  IIP SetContentInformationType(IPContentInformationType contentInformationType);
  IPContentInformationType GetContentInformationType();

  IIP SetStatus(IPStatus status);
  IPStatus GetStatus();

  IIP SetCreateDate(DateTime date);
  DateTime? GetCreateDate();

  IIP SetModificationDate(DateTime date);
  DateTime? GetModificationDate();

  IIP SetBasePath(string basePath);
  string? GetBasePath();

  IIP SetAncestors(List<string> ancestors);
  List<string> GetAncestors();

  IIP SetDescription(string description);
  string GetDescription();

  IIP AddAgent(IPAgent agent);
  IIP AddDescriptiveMetadata(IPDescriptiveMetadata descriptiveMetadata);
  IIP AddPreservationMetadata(IPMetadata preservationMetadata);
  IIP AddOtherMetadata(IPMetadata otherMetadata);
  IIP AddRepresentation(IPRepresentation representation);
  IIP AddSchema(IIPFile schema);
  IIP AddDocumentation(IIPFile documentation);

  IIP AddAgentToRepresentation(string representationId, IPAgent agent);
  IIP AddDescriptiveMetadataToRepresentation(string representationId, IPDescriptiveMetadata descriptiveMetadata);
  IIP AddPreservationMetadataToRepresentation(string representationId, IPMetadata preservationMetadata);
  IIP AddOtherMetadataToRepresentation(string representationId, IPMetadata otherMetadata);
  IIP AddFileToRepresentation(string representationId, IIPFile representation);
  IIP AddSchemaToRepresentation(string representationId, IIPFile schema);
  IIP AddDocumentationToRepresentation(string representationId, IIPFile documentation);

  List<IPAgent> GetAgents();
  List<IPDescriptiveMetadata> GetDescriptiveMetadata();
  List<IPMetadata> GetPreservationMetadata();
  List<IPMetadata> GetOtherMetadata();
  List<IPRepresentation> GetRepresentations();
  List<IIPFile> GetSchemas();
  List<IIPFile> GetDocumentation();

  Dictionary<string, IZipEntryInfo> GetZipEntries();
  IPHeader GetHeader();

  string Build(IWriteStrategy writeStrategy);
  string Build(IWriteStrategy writeStrategy, bool onlyManifest);
  string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension);
  string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, SIPType sipType);
  string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, bool onlyManifest);
  string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, bool onlyManifest, SIPType sipType);
}