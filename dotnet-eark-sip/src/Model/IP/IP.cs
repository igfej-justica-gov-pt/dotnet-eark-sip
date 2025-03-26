using IPEnums;
using Mets;
using Xml.Mets.CsipExtensionMets;

namespace IP
{
    /// <summary>
    /// Represents an abstract base class for Information Packages (IP).
    /// </summary>
    public abstract class IP : IIP
    {
        private List<string> ids;
        private string profile;
        private IPType type;
        private IPHeader header;

        // maps to mets/@type
        private IPContentType contentType;
        private IPContentInformationType contentInformationType;
        private List<string> ancestors;

        private string? basePath;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="IP"/> class.
        /// </summary>
        public IP()
        {
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

        /// <summary>
        /// Initializes a new instance of the <see cref="IP"/> class with specified IDs and type.
        /// </summary>
        /// <param name="ids">The list of IDs.</param>
        /// <param name="type">The type of the IP.</param>
        public IP(List<string> ids, IPType type) : this()
        {
            this.ids = new List<string>(ids);
            this.type = type;
            zipEntries = new Dictionary<string, IZipEntryInfo>();
            header = new IPHeader();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IP"/> class with specified IDs, type, and content type.
        /// </summary>
        /// <param name="ids">The list of IDs.</param>
        /// <param name="type">The type of the IP.</param>
        /// <param name="contentType">The content type of the IP.</param>
        public IP(List<string> ids, IPType type, IPContentType contentType) : this(ids, type)
        {
            this.contentType = contentType;
        }

        /// <summary>
        /// Gets the primary ID of the IP.
        /// </summary>
        /// <returns>The primary ID.</returns>
        public string GetId()
        {
            return ids.First() ?? "";
        }

        /// <summary>
        /// Sets the primary ID of the IP.
        /// </summary>
        /// <param name="id">The ID to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetId(string id)
        {
            ids = new List<string>() { id };
            return this;
        }

        /// <summary>
        /// Gets the list of IDs of the IP.
        /// </summary>
        /// <returns>The list of IDs.</returns>
        public List<string> GetIds()
        {
            return ids;
        }

        /// <summary>
        /// Sets the list of IDs of the IP.
        /// </summary>
        /// <param name="ids">The list of IDs to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetIds(List<string> ids)
        {
            this.ids = ids;
            return this;
        }

        /// <summary>
        /// Gets the profile of the IP.
        /// </summary>
        /// <returns>The profile.</returns>
        public string GetProfile()
        {
            return profile;
        }

        /// <summary>
        /// Sets the profile of the IP.
        /// </summary>
        /// <param name="profile">The profile to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetProfile(string profile)
        {
            this.profile = profile;
            return this;
        }


        /// <summary>
        /// Gets the type of the IP.
        /// </summary>
        /// <returns>The type of the IP.</returns>
        public string _GetType()
        {
            return type.ToString();
        }

        /// <summary>
        /// Sets the type of the IP.
        /// </summary>
        /// <param name="type">The type to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetType(IPType type)
        {
            this.type = type;
            return this;
        }

        /// <summary>
        /// Gets the header of the IP.
        /// </summary>
        /// <returns>The header.</returns>
        public IPHeader GetHeader()
        {
            return header;
        }

        /// <summary>
        /// Sets the header of the IP.
        /// </summary>
        /// <param name="header">The header to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetHeader(IPHeader header)
        {
            this.header = header;
            return this;
        }


        /// <summary>
        /// Gets the list of agents associated with the IP.
        /// </summary>
        /// <returns>The list of agents.</returns>
        public List<IPAgent> GetAgents() => header.GetAgents();

        /// <summary>
        /// Adds an agent to the IP.
        /// </summary>
        /// <param name="agent">The agent to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddAgent(IPAgent agent)
        {
            header.AddAgent(agent);
            return this;
        }

        /// <summary>
        /// Gets the status of the IP.
        /// </summary>
        /// <returns>The status.</returns>
        public IPStatus GetStatus() => header.GetStatus();

        /// <summary>
        /// Sets the status of the IP.
        /// </summary>
        /// <param name="status">The status to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetStatus(IPStatus status)
        {
            header.SetStatus(status);
            return this;
        }

        /// <summary>
        /// Gets the creation date of the IP.
        /// </summary>
        /// <returns>The creation date.</returns>
        public DateTime? GetCreateDate() => header.GetCreateDate();

        /// <summary>
        /// Sets the creation date of the IP.
        /// </summary>
        /// <param name="date">The creation date to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetCreateDate(DateTime date)
        {
            header.SetCreateDate(date);
            return this;
        }

        /// <summary>
        /// Gets the modification date of the IP.
        /// </summary>
        /// <returns>The modification date.</returns>
        public DateTime? GetModificationDate() => header.GetModificationDate();

        /// <summary>
        /// Sets the modification date of the IP.
        /// </summary>
        /// <param name="date">The modification date to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetModificationDate(DateTime date)
        {
            header.SetModificationDate(date);
            return this;
        }

        /// <summary>
        /// Gets the content type of the IP.
        /// </summary>
        /// <returns>The content type.</returns>
        public IPContentType GetContentType()
        {
            return contentType;
        }

        /// <summary>
        /// Sets the content type of the IP.
        /// </summary>
        /// <param name="contentType">The content type to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetContentType(IPContentType contentType)
        {
            this.contentType = contentType;
            return this;
        }

        /// <summary>
        /// Gets the content information type of the IP.
        /// </summary>
        /// <returns>The content information type.</returns>
        public IPContentInformationType GetContentInformationType()
        {
            return contentInformationType;
        }

        /// <summary>
        /// Sets the content information type of the IP.
        /// </summary>
        /// <param name="contentInformationType">The content information type to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetContentInformationType(IPContentInformationType contentInformationType)
        {
            this.contentInformationType = contentInformationType;
            return this;
        }

        /// <summary>
        /// Gets the list of ancestors of the IP.
        /// </summary>
        /// <returns>The list of ancestors.</returns>
        public List<string> GetAncestors()
        {
            return ancestors;
        }

        /// <summary>
        /// Sets the list of ancestors of the IP.
        /// </summary>
        /// <param name="ancestors">The list of ancestors to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetAncestors(List<string> ancestors)
        {
            this.ancestors = ancestors;
            return this;
        }

        /// <summary>
        /// Gets the base path of the IP.
        /// </summary>
        /// <returns>The base path.</returns>
        public string? GetBasePath()
        {
            return basePath;
        }

        /// <summary>
        /// Sets the base path of the IP.
        /// </summary>
        /// <param name="basePath">The base path to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetBasePath(string basePath)
        {
            this.basePath = basePath;
            return this;
        }

        /// <summary>
        /// Gets the description of the IP.
        /// </summary>
        /// <returns>The description.</returns>
        public string GetDescription()
        {
            return description;
        }

        /// <summary>
        /// Sets the description of the IP.
        /// </summary>
        /// <param name="description">The description to set.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP SetDescription(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// Gets the list of descriptive metadata associated with the IP.
        /// </summary>
        /// <returns>The list of descriptive metadata.</returns>
        public List<IPDescriptiveMetadata> GetDescriptiveMetadata()
        {
            return descriptiveMetadata;
        }

        /// <summary>
        /// Adds descriptive metadata to the IP.
        /// </summary>
        /// <param name="metadata">The descriptive metadata to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddDescriptiveMetadata(IPDescriptiveMetadata metadata)
        {
            descriptiveMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Gets the list of preservation metadata associated with the IP.
        /// </summary>
        /// <returns>The list of preservation metadata.</returns>
        public List<IPMetadata> GetPreservationMetadata()
        {
            return preservationMetadata;
        }

        /// <summary>
        /// Adds preservation metadata to the IP.
        /// </summary>
        /// <param name="metadata">The preservation metadata to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddPreservationMetadata(IPMetadata metadata)
        {
            preservationMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Gets the list of other metadata associated with the IP.
        /// </summary>
        /// <returns>The list of other metadata.</returns>
        public List<IPMetadata> GetOtherMetadata()
        {
            return otherMetadata;
        }

        /// <summary>
        /// Gets the list of representations associated with the IP.
        /// </summary>
        /// <returns>The list of representations.</returns>
        public List<IPRepresentation> GetRepresentations()
        {
            List<IPRepresentation> representationList = new List<IPRepresentation>();
            foreach (string id in representationIds)
            {
                representationList.Add(representations[id]);
            }

            return representationList;
        }

        /// <summary>
        /// Adds a representation to the IP.
        /// </summary>
        /// <param name="representation">The representation to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        /// <exception cref="IPException">Thrown when the representation already exists.</exception>
        public IIP AddRepresentation(IPRepresentation representation)
        {
            string representationID = representation.RepresentationID;
            if (representations.ContainsKey(representationID))
            {
                throw new IPException("Representation already exists");
            }
            else
            {
                representationIds.Add(representationID);
                representations.Add(representationID, representation);
                return this;
            }
        }

        /// <summary>
        /// Adds an agent to a specific representation.
        /// </summary>
        /// <param name="representationID">The ID of the representation.</param>
        /// <param name="agent">The agent to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddAgentToRepresentation(string representationID, IPAgent agent)
        {
            CheckIfRepresentationExists(representationID);
            IPRepresentation rep = representations[representationID];
            rep.AddAgent(agent);
            representations[representationID] = rep;
            return this;
        }

        /// <summary>
        /// Adds a file to a specific representation.
        /// </summary>
        /// <param name="representationID">The ID of the representation.</param>
        /// <param name="file">The file to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddFileToRepresentation(string representationID, IIPFile file)
        {
            CheckIfRepresentationExists(representationID);
            IPRepresentation rep = representations[representationID];
            rep.AddFile(file);
            representations[representationID] = rep;
            return this;
        }

        /// <summary>
        /// Adds descriptive metadata to a specific representation.
        /// </summary>
        /// <param name="representationID">The ID of the representation.</param>
        /// <param name="metadata">The descriptive metadata to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddDescriptiveMetadataToRepresentation(string representationID, IPDescriptiveMetadata metadata)
        {
            CheckIfRepresentationExists(representationID);
            IPRepresentation rep = representations[representationID];
            rep.AddDescriptiveMetadata(metadata);
            representations[representationID] = rep;
            return this;
        }

        /// <summary>
        /// Adds preservation metadata to a specific representation.
        /// </summary>
        /// <param name="representationID">The ID of the representation.</param>
        /// <param name="metadata">The preservation metadata to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddPreservationMetadataToRepresentation(string representationID, IPMetadata metadata)
        {
            CheckIfRepresentationExists(representationID);
            IPRepresentation rep = representations[representationID];
            rep.AddPreservationMetadata(metadata);
            representations[representationID] = rep;
            return this;
        }

        /// <summary>
        /// Adds other metadata to a specific representation.
        /// </summary>
        /// <param name="representationID">The ID of the representation.</param>
        /// <param name="metadata">The other metadata to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddOtherMetadataToRepresentation(string representationID, IPMetadata metadata)
        {
            CheckIfRepresentationExists(representationID);
            IPRepresentation rep = representations[representationID];
            rep.AddOtherMetadata(metadata);
            representations[representationID] = rep;
            return this;
        }

        /// <summary>
        /// Adds a schema to a specific representation.
        /// </summary>
        /// <param name="representationID">The ID of the representation.</param>
        /// <param name="schema">The schema to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddSchemaToRepresentation(string representationID, IIPFile schema)
        {
            CheckIfRepresentationExists(representationID);
            IPRepresentation rep = representations[representationID];
            rep.AddSchema(schema);
            representations[representationID] = rep;
            return this;
        }

        /// <summary>
        /// Adds documentation to a specific representation.
        /// </summary>
        /// <param name="representationID">The ID of the representation.</param>
        /// <param name="documentation">The documentation to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddDocumentationToRepresentation(string representationID, IIPFile documentation)
        {
            CheckIfRepresentationExists(representationID);
            IPRepresentation rep = representations[representationID];
            rep.AddDocumentation(documentation);
            representations[representationID] = rep;
            return this;
        }

        /// <summary>
        /// Add other metadata to the IP.
        /// </summary>
        /// <param name="metadata">Metadata to add</param>
        /// <returns></returns>
        public IIP AddOtherMetadata(IPMetadata metadata)
        {
            otherMetadata.Add(metadata);
            return this;
        }

        /// <summary>
        /// Gets the list of schemas associated with the IP.
        /// </summary>
        /// <returns>The list of schemas.</returns>
        public List<IIPFile> GetSchemas()
        {
            return schemas;
        }

        /// <summary>
        /// Adds a schema to the IP.
        /// </summary>
        /// <param name="schema">The schema to add.</param>
        /// <returns>The current instance of <see cref="IP"/>.</returns>
        public IIP AddSchema(IIPFile schema)
        {
            schemas.Add(schema);
            return this;
        }

        /// <summary>
        /// Gets the list of documentation associated with the IP.
        /// </summary>
        /// <returns>The list of documentation.</returns>
        public List<IIPFile> GetDocumentation()
        {
            return documentation;
        }

        /// <summary>
        /// Adds documentation to the IP.
        /// </summary>
        /// <param name="documentationPath">The documentation path to add</param>
        /// <returns></returns>
        public IIP AddDocumentation(IIPFile documentationPath)
        {
            documentation.Add(documentationPath);
            return this;
        }

        /// <summary>
        /// Get current zip entries
        /// </summary>
        /// <returns>Dictionary of string/zipentry</returns>
        public Dictionary<string, IZipEntryInfo> GetZipEntries()
        {
            return zipEntries;
        }

        /// <summary>
        /// Get current checksum algorithm
        /// </summary>
        /// <returns>The checksum type</returns>
        public IFilecoreChecksumtype GetChecksumAlgorithm()
        {
            return checksumAlgorithm;
        }

        /// <summary>
        /// Set checksum algorithm
        /// </summary>
        /// <param name="checksumAlgorithm">Algorithm to set</param>
        /// <returns>Current IP instance</returns>
        public IIP SetChecksumAlgorithm(IFilecoreChecksumtype checksumAlgorithm)
        {
            this.checksumAlgorithm = checksumAlgorithm;
            return this;
        }

        /// <summary>
        /// Set checksum algorithm (from string)
        /// </summary>
        /// <param name="checksumAlgorithm">Algorithm from string</param>
        /// <returns>Current IP instance</returns>
        public IIP SetChecksumAlgorithm(string checksumAlgorithm)
        {
            try
            {
                this.checksumAlgorithm = EnumUtils.GetEnumFromXmlAttribute<IFilecoreChecksumtype>(checksumAlgorithm);
            }
            catch
            {
                this.checksumAlgorithm = IFilecoreChecksumtype.SHA256;
            }

            return this;
        }

        public bool GetOverride()
        {
            return _override;
        }

        public IIP SetOverride()
        {
            _override = true;
            return this;
        }

        private void CheckIfRepresentationExists(string representationID)
        {
            if (!representations.ContainsKey(representationID))
            {
                throw new IPException("Representation does not exist");
            }
        }

        /// <summary>
        /// Add a creator agent to the IP
        /// </summary>
        /// <param name="name">The agent name</param>
        /// <param name="version">Version</param>
        /// <returns></returns>
        public IPAgent AddCreatorSoftwareAgent(string name, string version)
        {
            IPAgent creatorAgent = new IPAgent(name, MetsTypeMetsHdrAgentRole.CREATOR, null, MetsTypeMetsHdrAgentType.OTHER, "SOFTWARE", version, Notetype.SOFTWARE_VERSION);
            header.AddAgent(creatorAgent);
            return creatorAgent;
        }

        private IPAgent GetSubmitterDefaultAgent()
        {
            return new IPAgent("Default submitter agent", MetsTypeMetsHdrAgentRole.CREATOR, null, MetsTypeMetsHdrAgentType.INDIVIDUAL, null, "1", Notetype.IDENTIFICATIONCODE);
        }

        /// <summary>
        /// Add a submitter agent to the IP
        /// </summary>
        /// <param name="name">The submitter agent name</param>
        /// <param name="id">The identifier</param>
        /// <returns></returns>
        public IPAgent AddSubmitterAgent(string? name = null, string? id = null)
        {
            IPAgent submitterAgent = GetSubmitterDefaultAgent();

            if (name != null && name != "")
            {
                submitterAgent.SetName(name);
            }

            if (id != null && name != "")
            {
                submitterAgent.SetNote(id);
            }

            header.AddAgent(submitterAgent);
            return submitterAgent;
        }

        public abstract HashSet<IFilecoreChecksumtype> GetExtraChecksumAlgorithms();

        /// <summary>
        /// Build the IP, create the METS file and write the IP to disk
        /// </summary>
        /// <param name="writeStrategy">The write strategy (e.g. ZIP)</param>
        /// <returns></returns>
        public abstract string Build(IWriteStrategy writeStrategy);
        public abstract string Build(IWriteStrategy writeStrategy, bool onlyManifest);
        public abstract string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension);
        public abstract string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, SIPType sipType);
        public abstract string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, bool onliManifest);
        public abstract string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, bool onliManifest, SIPType sipType);

        public override string ToString()
        {
            return "IP [" +
              "ids=" + string.Join(", ", ids) + ", profile=" + profile + ", type=" + type + ", header=" + header + ", contentType="
              + contentType + ", ancestors=" + ancestors + ", basePath=" + basePath ?? " " + ", description=" + description
              + ", descriptiveMetadata=[" + string.Join(", ", descriptiveMetadata) + "], preservationMetadata=[" + string.Join(", ", preservationMetadata)
              + "], otherMetadata=[" + string.Join(", ", otherMetadata) + "], representationIds=[" + representationIds +
              ", representations=" + string.Join(", ", representationIds) + ", schemas=[" + string.Join(", ", schemas) +
              "], documentation=[" + string.Join(", ", documentation) + "]" +
            "]";
        }
    }
}