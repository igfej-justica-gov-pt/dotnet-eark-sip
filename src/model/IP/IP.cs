using IPEnums;
using Mets;

namespace IP {
  public abstract class IP {
    private List<string> ids;
    private string profile;
    private IPType type;
    private IPHeader header;

    // maps to mets/@type
    private IPContentType contentType;
    private IPContentInformationType contentInformationType;
    private List<string> ancestors;

    private string basePath;
    private string description;

    private readonly List<IPDescriptiveMetadata> descriptiveMetadata;
    private readonly List<IPMetadata> preservationMetadata;
    private readonly List<IPMetadata> otherMetadata;
    private readonly List<string> representationIds;
    private readonly Dictionary<string, IPRepresentation> representations;
    private readonly List<IIPFile> schemas;
    private readonly List<IIPFile> documentation;

    private readonly Dictionary<string, IZipEntryInfo> zipEntries;

    private string checksumAlgorithm;
    private bool _override;

    public IP() {
      ids = new List<string>() { Utils.GenerateRandomAndPrefixedUUID() };
      profile = "NOT_DEFINED";
      type = IPType.SIP;
      header = new IPHeader();

      contentType = IPContentType.GetMIXED();
      contentInformationType = IPContentInformationType.GetMIXED();
      ancestors = new List<string>();

      description = "";
      checksumAlgorithm = "SHA-256";

      descriptiveMetadata = new List<IPDescriptiveMetadata>();
      preservationMetadata = new List<IPMetadata>();
      otherMetadata = new List<IPMetadata>();
      representationIds = new List<string>();
      representations = new Dictionary<string, IPRepresentation>();
      schemas = new List<IIPFile>();
      documentation = new List<IIPFile>();

      zipEntries = new Dictionary<string, IZipEntryInfo>();
    }

    public IP(List<string> ids, IPType type): this() {
      this.ids = new List<string>(ids);
      this.type = type;
      zipEntries = new Dictionary<string, IZipEntryInfo>();
      header = new IPHeader();
    }

    public IP(List<string> ids, IPType type, IPContentType contentType) : this(ids, type) {
      this.contentType = contentType;
    }

    public string GetId() {
      return ids.First() ?? "";
    }

    public IP SetId(string id) {
      ids = new List<string>() { id };
      return this;
    }

    public List<string> GetIds() {
      return ids;
    }

    public IP SetIds(List<string> ids) {
      this.ids = ids;
      return this;
    }

    public string GetProfile() {
      return profile;
    }
    
    public IP SetProfile(string profile) {
      this.profile = profile;
      return this;
    }

    public string _GetType() {
      return type.ToString();
    }

    public IP SetType(IPType type) {
      this.type = type;
      return this;
    }

    public IPHeader GetHeader() {
      return header;
    }

    public IP SetHeader(IPHeader header) {
      this.header = header;
      return this;
    }

    public List<IPAgent> GetAgents() => header.GetAgents();
    public IP AddAgent(IPAgent agent) {
      header.AddAgent(agent);
      return this;
    }

    public IPStatus GetStatus() => header.GetStatus();
    public IP SetStatus(IPStatus status) {
      header.SetStatus(status);
      return this;
    }

    public DateTime? GetCreateDate() => header.GetCreateDate();
    public IP SetCreateDate(DateTime date) {
      header.SetCreateDate(date);
      return this;
    }

    public DateTime? GetModificationDate() => header.GetModificationDate();
    public IP SetModificationDate(DateTime date) {
      header.SetModificationDate(date);
      return this;
    }

    public IPContentType GetContentType() {
      return contentType;
    }

    public IP SetContentType(IPContentType contentType) {
      this.contentType = contentType;
      return this;
    }

    public IPContentInformationType GetContentInformationType() {
      return contentInformationType;
    }

    public IP SetContentInformationType(IPContentInformationType contentInformationType) {
      this.contentInformationType = contentInformationType;
      return this;
    }

    public List<string> GetAncestors() {
      return ancestors;
    }

    public IP SetAncestors(List<string> ancestors) {
      this.ancestors = ancestors;
      return this;
    }

    public string GetBasePath() {
      return basePath;
    }

    public IP SetBasePath(string basePath) {
      this.basePath = basePath;
      return this;
    }

    public string GetDescription() {
      return description;
    }

    public IP SetDescription(string description) {
      this.description = description;
      return this;
    }

    public List<IPDescriptiveMetadata> GetDescriptiveMetadata() {
      return descriptiveMetadata;
    }

    public IP AddDescriptiveMetadata(IPDescriptiveMetadata metadata) {
      descriptiveMetadata.Add(metadata);
      return this;
    }

    public List<IPMetadata> GetPreservationMetadata() {
      return preservationMetadata;
    }

    public IP AddPreservationMetadata(IPMetadata metadata) {
      preservationMetadata.Add(metadata);
      return this;
    }

    public List<IPMetadata> GetOtherMetadata() {
      return otherMetadata;
    }

    public List<IPRepresentation> GetRepresentations() {
      List<IPRepresentation> representationList = new List<IPRepresentation>();
      foreach(string id in representationIds) {
        representationList.Add(representations[id]);
      }

      return representationList;
    }

    public IP AddRepresentation(IPRepresentation representation) {
      string representationID = representation.RepresentationID;
      if (representations.ContainsKey(representationID)) {
        throw new IPException("Representation already exists");
      } else {
        representationIds.Add(representationID);
        representations.Add(representationID, representation);
        return this;
      }
    }

    public IP AddAgentToRepresentation(string representationID, IPAgent agent) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddAgent(agent);
      representations[representationID] = rep;
      return this;
    }

    public IP AddFileToRepresentation(string representationID, IIPFile file) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddFile(file);
      representations[representationID] = rep;
      return this;
    }

    public IP AddDescriptiveMetadataToRepresentation(string representationID, IPDescriptiveMetadata metadata) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddDescriptiveMetadata(metadata);
      representations[representationID] = rep;
      return this;
    }

    public IP AddPreservationMetadataToRepresentation(string representationID, IPMetadata metadata) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddPreservationMetadata(metadata);
      representations[representationID] = rep;
      return this;
    }

    public IP AddOtherMetadataToRepresentation(string representationID, IPMetadata metadata) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddOtherMetadata(metadata);
      representations[representationID] = rep;
      return this;
    }

    public IP AddSchemaToRepresentation(string representationID, IIPFile schema) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddSchema(schema);
      representations[representationID] = rep;
      return this;
    }

    public IP AddDocumentationToRepresentation(string representationID, IIPFile documentation) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddDocumentation(documentation);
      representations[representationID] = rep;
      return this;
    }

    public IP AddOtherMetadata(IPMetadata metadata) {
      otherMetadata.Add(metadata);
      return this;
    }

    public List<IIPFile> GetSchemas() {
      return new List<IIPFile>(schemas);
    }

    public IP AddSchema(IIPFile schema) {
      schemas.Add(schema);
      return this;
    }

    public List<IIPFile> GetDocumentation() {
      return new List<IIPFile>(documentation);
    }

    public IP AddDocumentation(IIPFile documentationPath) {
      documentation.Add(documentationPath);
      return this;
    }

    public Dictionary<string, IZipEntryInfo> GetZipEntries() {
      return zipEntries;
    }

    public string GetChecksumAlgorithm() {
      return checksumAlgorithm;
    }

    public IP SetChecksumAlgorithm(string checksumAlgorithm) {
      this.checksumAlgorithm = checksumAlgorithm;
      return this;
    }

    public bool GetOverride() {
      return _override;
    }

    public IP SetOverride(bool _override) {
      this._override = _override;
      return this;
    }

    private void CheckIfRepresentationExists(string representationID) {
      if (!representations.ContainsKey(representationID)) {
        throw new IPException("Representation does not exist");
      }
    }

    private IPAgent GetSubmitterDefaultAgent() {
      return new IPAgent("Default submitter agent", "CREATOR", null, MetsTypeMetsHdrAgentType.INDIVIDUAL, null, "1", IPAgentNoteTypeEnum.IDENTIFICATIONCODE);
    }

    public IPAgent AddCreatorSoftwareAgent(string name, string version) {
      IPAgent creatorAgent = new IPAgent(name, "CREATOR", null, MetsTypeMetsHdrAgentType.OTHER, "SOFTWARE", version, IPAgentNoteTypeEnum.SOFTWARE_VERSION);
      header.AddAgent(creatorAgent);
      return creatorAgent;
    }

    public IPAgent AddSubmitterAgent(string name, string id) {
      IPAgent submitterAgent = GetSubmitterDefaultAgent();

      if (!string.IsNullOrEmpty(name)) {
        submitterAgent.SetName(name);
      }
      
      if (!string.IsNullOrEmpty(id)) {
        submitterAgent.SetNote(id);
      }
      
      header.AddAgent(submitterAgent);
      return submitterAgent;
    }

    public abstract HashSet<string> GetExtraChecksumAlgorithms();

    public override string ToString()
    {
      return "IP [ids=" + ids + ", profile=" + profile + ", type=" + type + ", header=" + header + ", contentType="
        + contentType + ", ancestors=" + ancestors + ", basePath=" + basePath + ", description=" + description
        + ", descriptiveMetadata=" + descriptiveMetadata + ", preservationMetadata=" + preservationMetadata
        + ", otherMetadata=" + otherMetadata + ", representationIds=" + representationIds + ", representations="
        + representations + ", schemas=" + schemas + ", documentation=" + documentation +
      "]";
    }
  }
}