namespace IP
{
    /// <summary>
    /// Represents a Representation with its associated metadata, files, and agents.
    /// </summary>
    public class IPRepresentation
    {
        /// <summary>
        /// Gets the unique identifier for the representation.
        /// </summary>
        public string RepresentationID { get; private set; }
        private string objectID;

        private DateTime? createDate;
        private DateTime? modificationDate;

        private IPContentType contentType;
        /// <summary>
        /// Gets or sets the content information type of the representation.
        /// </summary>
        public IPContentInformationType ContentInformationType { get; set; }
        private RepresentationStatus status;
        private string description;

        /// <summary>
        /// Gets the list of agents associated with the representation.
        /// </summary>
        public List<IPAgent> Agents { get; private set; }

        /// <summary>
        /// Gets the list of descriptive metadata associated with the representation.
        /// </summary>
        public List<IPDescriptiveMetadata> DescriptiveMetadata { get; private set; }

        /// <summary>
        /// Gets the list of preservation metadata associated with the representation.
        /// </summary>
        public List<IPMetadata> PreservationMetadata { get; private set; }

        /// <summary>
        /// Gets the list of technical metadata associated with the representation.
        /// </summary>
        public List<IPMetadata> TechnicalMetadata { get; private set; }

        /// <summary>
        /// Gets the list of source metadata associated with the representation.
        /// </summary>
        public List<IPMetadata> SourceMetadata { get; private set; }

        /// <summary>
        /// Gets the list of rights metadata associated with the representation.
        /// </summary>
        public List<IPMetadata> RightsMetadata { get; private set; }

        /// <summary>
        /// Gets the list of other metadata associated with the representation.
        /// </summary>
        public List<IPMetadata> OtherMetadata { get; private set; }

        /// <summary>
        /// Gets the list of data files associated with the representation.
        /// </summary>
        public List<IIPFile> Data { get; private set; }

        /// <summary>
        /// Gets the list of schema files associated with the representation.
        /// </summary>
        public List<IIPFile> Schemas { get; private set; }

        /// <summary>
        /// Gets the list of documentation files associated with the representation.
        /// </summary>
        public List<IIPFile> Documentation { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IPRepresentation"/> class with an optional representation ID.
        /// </summary>
        /// <param name="representationID">The unique identifier for the representation. If null, a random UUID is generated.</param>
        public IPRepresentation(string? representationID = null) : base()
        {
            RepresentationID = representationID ?? Utils.GenerateRandomAndPrefixedUUID();
            objectID = RepresentationID;
            createDate = DateTime.Now;
            contentType = IPContentType.GetMIXED();
            ContentInformationType = IPContentInformationType.GetMIXED();
            status = RepresentationStatus.GetORIGINAL();
            description = string.Empty;
            Agents = new List<IPAgent>();
            DescriptiveMetadata = new List<IPDescriptiveMetadata>();
            PreservationMetadata = new List<IPMetadata>();
            TechnicalMetadata = new List<IPMetadata>();
            SourceMetadata = new List<IPMetadata>();
            RightsMetadata = new List<IPMetadata>();
            OtherMetadata = new List<IPMetadata>();
            Data = new List<IIPFile>();
            Schemas = new List<IIPFile>();
            Documentation = new List<IIPFile>();
        }

        /// <summary>
        /// Gets the object ID of the representation.
        /// </summary>
        /// <returns>The object ID as a string.</returns>
        public string GetObjectID()
        {
            return objectID;
        }

        /// <summary>
        /// Sets the object ID of the representation.
        /// </summary>
        /// <param name="objectID">The object ID to set.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation SetObjectID(string objectID)
        {
            this.objectID = objectID;
            return this;
        }

        /// <summary>
        /// Gets the content type of the representation.
        /// </summary>
        /// <returns>The content type as an <see cref="IPContentType"/>.</returns>
        public IPContentType GetContentType()
        {
            return contentType;
        }

        /// <summary>
        /// Sets the content type of the representation.
        /// </summary>
        /// <param name="contentType">The content type to set.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation SetContentType(IPContentType contentType)
        {
            this.contentType = contentType;
            return this;
        }

        /// <summary>
        /// Gets the status of the representation.
        /// </summary>
        /// <returns>The status as a string.</returns>
        public string GetStatus()
        {
            return status.AsString();
        }

        /// <summary>
        /// Sets the status of the representation.
        /// </summary>
        /// <param name="status">The status to set.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation SetStatus(RepresentationStatus status)
        {
            this.status = status;
            return this;
        }

        /// <summary>
        /// Gets the creation date of the representation.
        /// </summary>
        /// <returns>The creation date as a string.</returns>
        public DateTime? GetCreateDate()
        {
            return createDate;
        }

        /// <summary>
        /// Sets the creation date of the representation.
        /// </summary>
        /// <param name="createDate">The creation date to set.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation SetCreateDate(DateTime? createDate)
        {
            this.createDate = createDate;
            return this;
        }

        /// <summary>
        /// Gets the last modification date of the representation.
        /// </summary>
        /// <returns>The last modification date as a string.</returns>
        public DateTime? GetModificationDate()
        {
            return modificationDate;
        }

        /// <summary>
        /// Sets the last modification date of the representation.
        /// </summary>
        /// <param name="modificationDate">The last modification date to set.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation SetModificationDate(DateTime? modificationDate)
        {
            this.modificationDate = modificationDate;
            return this;
        }

        /// <summary>
        /// Gets the description of the representation.
        /// </summary>
        /// <returns>The description as a string.</returns>
        public string GetDescription()
        {
            return description;
        }

        /// <summary>
        /// Sets the description of the representation.
        /// </summary>
        /// <param name="description">The description to set.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation SetDescription(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// Adds an agent to the list of agents associated with the representation.
        /// </summary>
        /// <param name="agent">The agent to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddAgent(IPAgent agent)
        {
            Agents.Add(agent);
            return this;
        }

        /// <summary>
        /// Adds a descriptive metadata instance to the list associated with the representation.
        /// </summary>
        /// <param name="metadata">The descriptive metadata instance to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddDescriptiveMetadata(IPDescriptiveMetadata metadata)
        {
            DescriptiveMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Adds a preservation metadata instance to the list associated with the representation.
        /// </summary>
        /// <param name="metadata">The preservation metadata instance to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddPreservationMetadata(IPMetadata metadata)
        {
            PreservationMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Adds a technical metadata instance to the list associated with the representation.
        /// </summary>
        /// <param name="metadata">The technical metadata instance to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddTechnicalMetadata(IPMetadata metadata)
        {
            TechnicalMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Adds a source metadata instance to the list associated with the representation.
        /// </summary>
        /// <param name="metadata">The source metadata instance to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddSourceMetadata(IPMetadata metadata)
        {
            SourceMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Adds a rights metadata instance to the list associated with the representation.
        /// </summary>
        /// <param name="metadata">The rights metadata instance to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddRightsMetadata(IPMetadata metadata)
        {
            RightsMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Adds a (other) metadata instance to the list associated with the representation.
        /// </summary>
        /// <param name="metadata">The (other) metadata instance to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddOtherMetadata(IPMetadata metadata)
        {
            OtherMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Adds a file to the list of data files associated with the representation.
        /// </summary>
        /// <param name="ipFile">The file to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddFile(IIPFile ipFile)
        {
            Data.Add(ipFile);
            return this;
        }

        /// <summary>
        /// Creates an instance of <see cref="IPFile"/> and adds it to the list of data files associated with the representation.
        /// </summary>
        /// <param name="filePath">The path of the file to add.</param>
        /// <param name="folders">The list of folders associated with the file.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddFile(string filePath, List<string> folders)
        {
            Data.Add(new IPFile(filePath, folders));
            return this;
        }

        /// <summary>
        /// Adds a schema file to the list of schema files associated with the representation.
        /// </summary>
        /// <param name="schema">The schema file to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddSchema(IIPFile schema)
        {
            Schemas.Add(schema);
            return this;
        }

        /// <summary>
        /// Adds a documentation file to the list of documentation files associated with the representation.
        /// </summary>
        /// <param name="documentation">The documentation file to add.</param>
        /// <returns>The current instance of <see cref="IPRepresentation"/>.</returns>
        public IPRepresentation AddDocumentation(IIPFile documentation)
        {
            Documentation.Add(documentation);
            return this;
        }

        /// <summary>
        /// Returns a string representation of the IPMetadata object.
        /// </summary>
        /// <returns>A string that represents the current IPMetadata object.</returns>
        public override string ToString()
        {
            return "IPRepresentation [" +
              "representationID=" + RepresentationID + ", objectID=" + objectID + ", createDate=" + createDate ?? " " +
              ", modificationDate=" + modificationDate ?? " " + ", contentType=" + contentType +
              ", contentInformationType=" + ContentInformationType + ", status=" + status + ", description=" + description +
              ", agents=[" + string.Join(", ", Agents) + "], descriptiveMetadata=[" + string.Join(", ", DescriptiveMetadata) +
              "], preservationMetadata=[" + string.Join(", ", PreservationMetadata) + "], technicalMetadata=[" + string.Join(", ", TechnicalMetadata) +
              "], sourceMetadata=[" + string.Join(", ", SourceMetadata) + "], rightsMetadata=[" + string.Join(", ", RightsMetadata) + "], otherMetadata=[" + string.Join(", ", OtherMetadata) +
              "], data=[" + string.Join(", ", Data) + "], schemas=[" + string.Join(", ", Schemas) + "], documentation=[" + string.Join(", ", Documentation) + "]" +
            "]";
        }
    }
}