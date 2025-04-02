using IP;
using IPEnums;

/// <summary>
/// Represents the interface for an Information Package (IP).
/// </summary>
public interface IIP
{
    /// <summary>
    /// Sets the identifier for the Information Package.
    /// </summary>
    /// <param name="id">The identifier to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetId(string id);
    /// <summary>
    /// Gets the identifier of the Information Package.
    /// </summary>
    /// <returns>The identifier of the Information Package.</returns>
    string GetId();

    /// <summary>
    /// Sets multiple identifiers for the Information Package.
    /// </summary>
    /// <param name="ids">The list of identifiers to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetIds(List<string> ids);
    /// <summary>
    /// Gets the list of identifiers of the Information Package.
    /// </summary>
    /// <returns>The list of identifiers.</returns>
    List<string> GetIds();

    /// <summary>
    /// Sets the profile for the Information Package.
    /// </summary>
    /// <param name="profile">The profile to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetProfile(string profile);
    /// <summary>
    /// Gets the profile of the Information Package.
    /// </summary>
    /// <returns>The profile of the Information Package.</returns>
    string GetProfile();

    /// <summary>
    /// Sets the type of the Information Package.
    /// </summary>
    /// <param name="type">The type to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetType(IPType type);
    /// <summary>
    /// Gets the type of the Information Package.
    /// </summary>
    /// <returns>The type of the Information Package.</returns>
    string _GetType();

    /// <summary>
    /// Sets the content type of the Information Package.
    /// </summary>
    /// <param name="contentType">The content type to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetContentType(IPContentType contentType);
    /// <summary>
    /// Gets the content type of the Information Package.
    /// </summary>
    /// <returns>The content type of the Information Package.</returns>
    IPContentType GetContentType();

    /// <summary>
    /// Sets the content information type of the Information Package.
    /// </summary>
    /// <param name="contentInformationType">The content information type to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetContentInformationType(IPContentInformationType contentInformationType);
    /// <summary>
    /// Gets the content information type of the Information Package.
    /// </summary>
    /// <returns>The content information type of the Information Package.</returns>
    IPContentInformationType GetContentInformationType();

    /// <summary>
    /// Sets the status of the Information Package.
    /// </summary>
    /// <param name="status">The status to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetStatus(IPStatus status);
    /// <summary>
    /// Gets the status of the Information Package.
    /// </summary>
    /// <returns>The status of the Information Package.</returns>
    IPStatus GetStatus();

    /// <summary>
    /// Sets the creation date of the Information Package.
    /// </summary>
    /// <param name="date">The creation date to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetCreateDate(DateTime date);
    /// <summary>
    /// Gets the creation date of the Information Package.
    /// </summary>
    /// <returns>The creation date of the Information Package.</returns>
    DateTime? GetCreateDate();

    /// <summary>
    /// Sets the modification date of the Information Package.
    /// </summary>
    /// <param name="date">The modification date to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetModificationDate(DateTime date);
    /// <summary>
    /// Gets the modification date of the Information Package.
    /// </summary>
    /// <returns>The modification date of the Information Package.</returns>
    DateTime? GetModificationDate();

    /// <summary>
    /// Sets the base path of the Information Package.
    /// </summary>
    /// <param name="basePath">The base path to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetBasePath(string basePath);
    /// <summary>
    /// Gets the base path of the Information Package.
    /// </summary>
    /// <returns>The base path of the Information Package.</returns>
    string? GetBasePath();

    /// <summary>
    /// Sets the ancestors of the Information Package.
    /// </summary>
    /// <param name="ancestors">The list of ancestors to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetAncestors(List<string> ancestors);
    /// <summary>
    /// Gets the ancestors of the Information Package.
    /// </summary>
    /// <returns>The list of ancestors of the Information Package.</returns>
    List<string> GetAncestors();

    /// <summary>
    /// Sets the description of the Information Package.
    /// </summary>
    /// <param name="description">The description to set.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP SetDescription(string description);
    /// <summary>
    /// Gets the description of the Information Package.
    /// </summary>
    /// <returns>The description of the Information Package.</returns>
    string GetDescription();

    /// <summary>
    /// Adds an agent to the Information Package.
    /// </summary>
    /// <param name="agent">The agent to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddAgent(IPAgent agent);

    /// <summary>
    /// Adds descriptive metadata to the Information Package.
    /// </summary>
    /// <param name="descriptiveMetadata">The descriptive metadata to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddDescriptiveMetadata(IPDescriptiveMetadata descriptiveMetadata);

    /// <summary>
    /// Adds preservation metadata to the Information Package.
    /// </summary>
    /// <param name="preservationMetadata">The preservation metadata to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddPreservationMetadata(IPMetadata preservationMetadata);

    /// <summary>
    /// Adds other metadata to the Information Package.
    /// </summary>
    /// <param name="otherMetadata">The other metadata to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddOtherMetadata(IPMetadata otherMetadata);

    /// <summary>
    /// Adds a representation to the Information Package.
    /// </summary>
    /// <param name="representation">The representation to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddRepresentation(IPRepresentation representation);

    /// <summary>
    /// Adds a schema to the Information Package.
    /// </summary>
    /// <param name="schema">The schema to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddSchema(IIPFile schema);

    /// <summary>
    /// Adds documentation to the Information Package.
    /// </summary>
    /// <param name="documentation">The documentation to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddDocumentation(IIPFile documentation);

    /// <summary>
    /// Adds an agent to a specific representation in the Information Package.
    /// </summary>
    /// <param name="representationId">The identifier of the representation.</param>
    /// <param name="agent">The agent to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddAgentToRepresentation(string representationId, IPAgent agent);

    /// <summary>
    /// Adds descriptive metadata to a specific representation in the Information Package.
    /// </summary>
    /// <param name="representationId">The identifier of the representation.</param>
    /// <param name="descriptiveMetadata">The descriptive metadata to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddDescriptiveMetadataToRepresentation(string representationId, IPDescriptiveMetadata descriptiveMetadata);

    /// <summary>
    /// Adds preservation metadata to a specific representation in the Information Package.
    /// </summary>
    /// <param name="representationId">The identifier of the representation.</param>
    /// <param name="preservationMetadata">The preservation metadata to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddPreservationMetadataToRepresentation(string representationId, IPMetadata preservationMetadata);

    /// <summary>
    /// Adds other metadata to a specific representation in the Information Package.
    /// </summary>
    /// <param name="representationId">The identifier of the representation.</param>
    /// <param name="otherMetadata">The other metadata to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddOtherMetadataToRepresentation(string representationId, IPMetadata otherMetadata);

    /// <summary>
    /// Adds a file to a specific representation in the Information Package.
    /// </summary>
    /// <param name="representationId">The identifier of the representation.</param>
    /// <param name="representation">The file to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddFileToRepresentation(string representationId, IIPFile representation);

    /// <summary>
    /// Adds a schema to a specific representation in the Information Package.
    /// </summary>
    /// <param name="representationId">The identifier of the representation.</param>
    /// <param name="schema">The schema to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddSchemaToRepresentation(string representationId, IIPFile schema);

    /// <summary>
    /// Adds documentation to a specific representation in the Information Package.
    /// </summary>
    /// <param name="representationId">The identifier of the representation.</param>
    /// <param name="documentation">The documentation to add.</param>
    /// <returns>The current instance of <see cref="IIP"/>.</returns>
    IIP AddDocumentationToRepresentation(string representationId, IIPFile documentation);

    /// <summary>
    /// Gets the list of agents associated with the Information Package.
    /// </summary>
    /// <returns>The list of agents.</returns>
    List<IPAgent> GetAgents();

    /// <summary>
    /// Gets the descriptive metadata associated with the Information Package.
    /// </summary>
    /// <returns>The list of descriptive metadata.</returns>
    List<IPDescriptiveMetadata> GetDescriptiveMetadata();

    /// <summary>
    /// Gets the preservation metadata associated with the Information Package.
    /// </summary>
    /// <returns>The list of preservation metadata.</returns>
    List<IPMetadata> GetPreservationMetadata();

    /// <summary>
    /// Gets the other metadata associated with the Information Package.
    /// </summary>
    /// <returns>The list of other metadata.</returns>
    List<IPMetadata> GetOtherMetadata();

    /// <summary>
    /// Gets the representations associated with the Information Package.
    /// </summary>
    /// <returns>The list of representations.</returns>
    List<IPRepresentation> GetRepresentations();

    /// <summary>
    /// Gets the schemas associated with the Information Package.
    /// </summary>
    /// <returns>The list of schemas.</returns>
    List<IIPFile> GetSchemas();

    /// <summary>
    /// Gets the documentation associated with the Information Package.
    /// </summary>
    /// <returns>The list of documentation files.</returns>
    List<IIPFile> GetDocumentation();

    /// <summary>
    /// Gets the zip entries associated with the Information Package.
    /// </summary>
    /// <returns>A dictionary containing zip entry information.</returns>
    Dictionary<string, IZipEntryInfo> GetZipEntries();

    /// <summary>
    /// Gets the header of the Information Package.
    /// </summary>
    /// <returns>The header of the Information Package.</returns>
    IPHeader GetHeader();

    /// <summary>
    /// Builds the Information Package using the specified write strategy.
    /// </summary>
    /// <param name="writeStrategy">The write strategy to use.</param>
    /// <returns>The path to the built Information Package.</returns>
    string Build(IWriteStrategy writeStrategy);

    /// <summary>
    /// Builds the Information Package using the specified write strategy and manifest option.
    /// </summary>
    /// <param name="writeStrategy">The write strategy to use.</param>
    /// <param name="onlyManifest">Indicates whether to build only the manifest.</param>
    /// <returns>The path to the built Information Package.</returns>
    string Build(IWriteStrategy writeStrategy, bool onlyManifest);

    /// <summary>
    /// Builds the Information Package using the specified write strategy and file name.
    /// </summary>
    /// <param name="writeStrategy">The write strategy to use.</param>
    /// <param name="fileNameWithoutExtension">The file name without extension.</param>
    /// <returns>The path to the built Information Package.</returns>
    string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension);

    /// <summary>
    /// Builds the Information Package using the specified write strategy, file name, and SIP type.
    /// </summary>
    /// <param name="writeStrategy">The write strategy to use.</param>
    /// <param name="fileNameWithoutExtension">The file name without extension.</param>
    /// <param name="sipType">The SIP type to use.</param>
    /// <returns>The path to the built Information Package.</returns>
    string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, SIPType sipType);

    /// <summary>
    /// Builds the Information Package using the specified write strategy, file name, and manifest option.
    /// </summary>
    /// <param name="writeStrategy">The write strategy to use.</param>
    /// <param name="fileNameWithoutExtension">The file name without extension.</param>
    /// <param name="onlyManifest">Indicates whether to build only the manifest.</param>
    /// <returns>The path to the built Information Package.</returns>
    string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, bool onlyManifest);

    /// <summary>
    /// Builds the Information Package using the specified write strategy, file name, manifest option, and SIP type.
    /// </summary>
    /// <param name="writeStrategy">The write strategy to use.</param>
    /// <param name="fileNameWithoutExtension">The file name without extension.</param>
    /// <param name="onlyManifest">Indicates whether to build only the manifest.</param>
    /// <param name="sipType">The SIP type to use.</param>
    /// <returns>The path to the built Information Package.</returns>
    string Build(IWriteStrategy writeStrategy, string fileNameWithoutExtension, bool onlyManifest, SIPType sipType);
}