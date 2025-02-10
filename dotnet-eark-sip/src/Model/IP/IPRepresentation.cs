namespace IP {
  public class IPRepresentation {
    public string RepresentationID { get; private set; }
    private string objectID;

    private DateTime? createDate;
    private DateTime? modificationDate;

    private IPContentType contentType;
    public IPContentInformationType ContentInformationType { get; set; }
    private RepresentationStatus status;
    private string description;
    public List<IPAgent> Agents { get; private set; }
    public List<IPDescriptiveMetadata> DescriptiveMetadata { get; private set; }
    public List<IPMetadata> PreservationMetadata { get; private set; }
    public List<IPMetadata> OtherMetadata { get; private set; }
    public List<IIPFile> Data { get; private set; }
    public List<IIPFile> Schemas { get; private set; }
    public List<IIPFile> Documentation { get; private set; }

    public IPRepresentation() {
      RepresentationID = Utils.GenerateRandomAndPrefixedUUID();
      objectID = RepresentationID;
      createDate = DateTime.Now;
      contentType = IPContentType.GetMIXED();
      ContentInformationType = IPContentInformationType.GetMIXED();
      status = RepresentationStatus.GetORIGINAL();
      description = string.Empty;
      Agents = new List<IPAgent>();
      DescriptiveMetadata = new List<IPDescriptiveMetadata>();
      PreservationMetadata = new List<IPMetadata>();
      OtherMetadata = new List<IPMetadata>();
      Data = new List<IIPFile>();
      Schemas = new List<IIPFile>();
      Documentation = new List<IIPFile>();
    }

    public IPRepresentation(string representationID) : base() {
      RepresentationID = representationID;
      objectID = representationID;
      createDate = DateTime.Now;
      contentType = IPContentType.GetMIXED();
      ContentInformationType = IPContentInformationType.GetMIXED();
      status = RepresentationStatus.GetORIGINAL();
      description = string.Empty;
      Agents = new List<IPAgent>();
      DescriptiveMetadata = new List<IPDescriptiveMetadata>();
      PreservationMetadata = new List<IPMetadata>();
      OtherMetadata = new List<IPMetadata>();
      Data = new List<IIPFile>();
      Schemas = new List<IIPFile>();
      Documentation = new List<IIPFile>();
    }

    public string GetObjectID() {
      return objectID;
    }

    public IPRepresentation SetObjectID(string objectID) {
      this.objectID = objectID;
      return this;
    }

    public IPContentType GetContentType() {
      return contentType;
    }

    public IPRepresentation SetContentType(IPContentType contentType) {
      this.contentType = contentType;
      return this;
    }

    public string GetStatus() {
      return status.AsString();
    }

    public IPRepresentation SetStatus(RepresentationStatus status) {
      this.status = status;
      return this;
    }

    public DateTime? GetCreateDate() {
      return createDate;
    }

    public IPRepresentation SetCreateDate(DateTime? createDate) {
      this.createDate = createDate;
      return this;
    }

    public DateTime? GetModificationDate() {
      return modificationDate;
    }

    public IPRepresentation SetModificationDate(DateTime? modificationDate) {
      this.modificationDate = modificationDate;
      return this;
    }

    public string GetDescription() {
      return description;
    }

    public IPRepresentation SetDescription(string description) {
      this.description = description;
      return this;
    }

    public IPRepresentation AddAgent(IPAgent agent) {
      Agents.Add(agent);
      return this;
    }

    public IPRepresentation AddDescriptiveMetadata(IPDescriptiveMetadata metadata) {
      DescriptiveMetadata.Add(metadata);
      return this;
    }

    public IPRepresentation AddPreservationMetadata(IPMetadata metadata) {
      PreservationMetadata.Add(metadata);
      return this;
    }

    public IPRepresentation AddOtherMetadata(IPMetadata metadata) {
      OtherMetadata.Add(metadata);
      return this;
    }

    public IPRepresentation AddFile(IIPFile ipFile) {
      Data.Add(ipFile);
      return this;
    }

    public IPRepresentation AddFile(string filePath, List<string> folders) {
      Data.Add(new IPFile(filePath, folders));
      return this;
    }

    public IPRepresentation AddSchema(IIPFile schema) {
      Schemas.Add(schema);
      return this;
    }

    public IPRepresentation AddDocumentation(IIPFile documentation) {
      Documentation.Add(documentation);
      return this;
    }

    public override string ToString() {
      return "IPRepresentation [representationID=" + RepresentationID + ", objectID=" + objectID + ", createDate="
        + createDate + ", modificationDate=" + modificationDate + ", contentType=" + contentType
        + ", contentInformationType=" + ContentInformationType + ", status=" + status + ", description=" + description
        + ", agents=" + Agents + ", descriptiveMetadata=" + DescriptiveMetadata + ", preservationMetadata="
        + PreservationMetadata + ", otherMetadata=" + OtherMetadata + ", data=" + Data + ", schemas=" + Schemas
        + ", documentation=" + Documentation + "]";
    }
  }
}