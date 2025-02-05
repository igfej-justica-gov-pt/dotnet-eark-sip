using IPEnums;
using Mets;
using Xml.Mets.CsipExtensionMets;

namespace IP {
  public abstract class IP : IIP {
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

    private IFilecoreChecksumtype checksumAlgorithm;
    private bool _override;

    public IP() {
      ids = new List<string>() { Utils.GenerateRandomAndPrefixedUUID() };
      profile = "NOT_DEFINED";
      type = IPType.SIP;
      header = new IPHeader();

      contentType = IPContentType.GetMIXED();
      contentInformationType = IPContentInformationType.GetMIXED();
      ancestors = new List<string>();

      description = string.Empty;
      checksumAlgorithm = IFilecoreChecksumtype.SHA256;

      descriptiveMetadata = new List<IPDescriptiveMetadata>();
      preservationMetadata = new List<IPMetadata>();
      otherMetadata = new List<IPMetadata>();
      representationIds = new List<string>();
      representations = new Dictionary<string, IPRepresentation>();
      schemas = new List<IIPFile>();
      documentation = new List<IIPFile>();

      zipEntries = new Dictionary<string, IZipEntryInfo>();
    }

    public IP(List<string> ids, IPType type) : this() {
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

    public IIP SetId(string id) {
      ids = new List<string>() { id };
      return this;
    }

    public List<string> GetIds() {
      return ids;
    }

    public IIP SetIds(List<string> ids) {
      this.ids = ids;
      return this;
    }

    public string GetProfile() {
      return profile;
    }
    
    public IIP SetProfile(string profile) {
      this.profile = profile;
      return this;
    }

    public string _GetType() {
      return type.ToString();
    }

    public IIP SetType(IPType type) {
      this.type = type;
      return this;
    }

    public IPHeader GetHeader() {
      return header;
    }

    public IIP SetHeader(IPHeader header) {
      this.header = header;
      return this;
    }

    public List<IPAgent> GetAgents() => header.GetAgents();
    public IIP AddAgent(IPAgent agent) {
      header.AddAgent(agent);
      return this;
    }

    public IPStatus GetStatus() => header.GetStatus();
    public IIP SetStatus(IPStatus status) {
      header.SetStatus(status);
      return this;
    }

    public DateTime? GetCreateDate() => header.GetCreateDate();
    public IIP SetCreateDate(DateTime date) {
      header.SetCreateDate(date);
      return this;
    }

    public DateTime? GetModificationDate() => header.GetModificationDate();
    public IIP SetModificationDate(DateTime date) {
      header.SetModificationDate(date);
      return this;
    }

    public IPContentType GetContentType() {
      return contentType;
    }

    public IIP SetContentType(IPContentType contentType) {
      this.contentType = contentType;
      return this;
    }

    public IPContentInformationType GetContentInformationType() {
      return contentInformationType;
    }

    public IIP SetContentInformationType(IPContentInformationType contentInformationType) {
      this.contentInformationType = contentInformationType;
      return this;
    }

    public List<string> GetAncestors() {
      return ancestors;
    }

    public IIP SetAncestors(List<string> ancestors) {
      this.ancestors = ancestors;
      return this;
    }

    public string GetBasePath() {
      return basePath;
    }

    public IIP SetBasePath(string basePath) {
      this.basePath = basePath;
      return this;
    }

    public string GetDescription() {
      return description;
    }

    public IIP SetDescription(string description) {
      this.description = description;
      return this;
    }

    public List<IPDescriptiveMetadata> GetDescriptiveMetadata() {
      return descriptiveMetadata;
    }

    public IIP AddDescriptiveMetadata(IPDescriptiveMetadata metadata) {
      descriptiveMetadata.Add(metadata);
      return this;
    }

    public List<IPMetadata> GetPreservationMetadata() {
      return preservationMetadata;
    }

    public IIP AddPreservationMetadata(IPMetadata metadata) {
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

    public IIP AddRepresentation(IPRepresentation representation) {
      string representationID = representation.RepresentationID;
      if (representations.ContainsKey(representationID)) {
        throw new IPException("Representation already exists");
      } else {
        representationIds.Add(representationID);
        representations.Add(representationID, representation);
        return this;
      }
    }

    public IIP AddAgentToRepresentation(string representationID, IPAgent agent) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddAgent(agent);
      representations[representationID] = rep;
      return this;
    }

    public IIP AddFileToRepresentation(string representationID, IIPFile file) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddFile(file);
      representations[representationID] = rep;
      return this;
    }

    public IIP AddDescriptiveMetadataToRepresentation(string representationID, IPDescriptiveMetadata metadata) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddDescriptiveMetadata(metadata);
      representations[representationID] = rep;
      return this;
    }

    public IIP AddPreservationMetadataToRepresentation(string representationID, IPMetadata metadata) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddPreservationMetadata(metadata);
      representations[representationID] = rep;
      return this;
    }

    public IIP AddOtherMetadataToRepresentation(string representationID, IPMetadata metadata) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddOtherMetadata(metadata);
      representations[representationID] = rep;
      return this;
    }

    public IIP AddSchemaToRepresentation(string representationID, IIPFile schema) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddSchema(schema);
      representations[representationID] = rep;
      return this;
    }

    public IIP AddDocumentationToRepresentation(string representationID, IIPFile documentation) {
      CheckIfRepresentationExists(representationID);
      IPRepresentation rep = representations[representationID];
      rep.AddDocumentation(documentation);
      representations[representationID] = rep;
      return this;
    }

    public IIP AddOtherMetadata(IPMetadata metadata) {
      otherMetadata.Add(metadata);
      return this;
    }

    public List<IIPFile> GetSchemas() {
      return schemas;
    }

    public IIP AddSchema(IIPFile schema) {
      schemas.Add(schema);
      return this;
    }

    public List<IIPFile> GetDocumentation() {
      return documentation;
    }

    public IIP AddDocumentation(IIPFile documentationPath) {
      documentation.Add(documentationPath);
      return this;
    }

    public Dictionary<string, IZipEntryInfo> GetZipEntries() {
      return zipEntries;
    }

    public IFilecoreChecksumtype GetChecksumAlgorithm() {
      return checksumAlgorithm;
    }

    public IIP SetChecksumAlgorithm(IFilecoreChecksumtype checksumAlgorithm) {
      this.checksumAlgorithm = checksumAlgorithm;
      return this;
    }

    public bool GetOverride() {
      return _override;
    }

    public IIP SetOverride() {
      _override = true;
      return this;
    }

    private void CheckIfRepresentationExists(string representationID) {
      if (!representations.ContainsKey(representationID)) {
        throw new IPException("Representation does not exist");
      }
    }

    private IPAgent GetSubmitterDefaultAgent() {
      return new IPAgent("Default submitter agent", MetsTypeMetsHdrAgentRole.CREATOR, null, MetsTypeMetsHdrAgentType.INDIVIDUAL, null, "1", Notetype.IDENTIFICATIONCODE);
    }

    public IPAgent AddCreatorSoftwareAgent(string name, string version) {
      IPAgent creatorAgent = new IPAgent(name, MetsTypeMetsHdrAgentRole.CREATOR, null, MetsTypeMetsHdrAgentType.OTHER, "SOFTWARE", version, Notetype.SOFTWARE_VERSION);
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

    public abstract HashSet<IFilecoreChecksumtype> GetExtraChecksumAlgorithms();

    public abstract string Build(IWriteStrategy writeStrategy);
    public abstract string Build(IWriteStrategy writeStrategy, bool onlyManifest);
    public abstract string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension);
    public abstract string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, SIPType sipType);
    public abstract string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, bool onliManifest);
    public abstract string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, bool onliManifest, SIPType sipType);

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