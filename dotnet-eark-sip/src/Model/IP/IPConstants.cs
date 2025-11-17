
/// <summary>
/// A class containing constants related to Information Packages (IP).
/// </summary>
public class IPConstants
{
    // METS related
    /// <summary>
    /// The name of the METS file.
    /// </summary>
    public static readonly string METS_FILE_NAME = "METS";

    /// <summary>
    /// The file extension for METS files.
    /// </summary>
    public static readonly string METS_FILE_EXTENSION = ".xml";

    /// <summary>
    /// The full name of the METS file, including its extension.
    /// </summary>
    public static readonly string METS_FILE = METS_FILE_NAME + METS_FILE_EXTENSION;

    /// <summary>
    /// The list of acceptable prefixes for METS file paths.
    /// </summary>
    public static readonly List<string> METS_FILE_PREFIXES_TO_ACCEPT = new List<string> { "file://./", "file:" };

    /// <summary>
    /// The simple type for METS.
    /// </summary>
    public static readonly string METS_TYPE_SIMPLE = "simple";

    /// <summary>
    /// The physical type for METS.
    /// </summary>
    public static readonly string METS_TYPE_PHYSICAL = "PHYSICAL";
    /// <summary>
    /// The separator used in METS paths.
    /// </summary>
    public static readonly string METS_PATH_SEPARATOR = "/";
    /// <summary>
    /// Representation type part 1 for METS.
    /// </summary>
    public static readonly string METS_REPRESENTATION_TYPE_PART_1 = "representation";

    /// <summary>
    /// Original METS type.
    /// </summary>
    public static readonly string METS_ORIGINAL = "original";

    /// <summary>
    /// Current status for METS.
    /// </summary>
    public static readonly string METS_STATUS_CURRENT = "current";

    /// <summary>
    /// Group ID for METS.
    /// </summary>
    public static readonly string METS_GROUP_ID = "1";

    /// <summary>
    /// Aggregation type for METS.
    /// </summary>
    public static readonly string METS_TYPE_AGGREGATION = "aggregation";

    /// <summary>
    /// Documentation type for METS.
    /// </summary>
    public static readonly string METS_TYPE_DOCUMENTATION = "documentation";

    /// <summary>
    /// Record group type for METS.
    /// </summary>
    public static readonly string METS_TYPE_RECORDGRP = "recordgrp";

    /// <summary>
    /// Label for Doku in METS.
    /// </summary>
    public static readonly string METS_LABEL_DOKU = "Doku";

    /// <summary>
    /// Other level for METS.
    /// </summary>
    public static readonly string METS_LEVEL_OTHER = "otherlevel";

    /// <summary>
    /// Default EAD type for METS.
    /// </summary>
    public static readonly string METS_EAD_TYPE = "EAD";

    /// <summary>
    /// Default EAD version for METS.
    /// </summary>
    public static readonly string METS_EAD_VERSION = "2002";

    /// <summary>
    /// ID field for folder templates.
    /// </summary>
    public static readonly string FOLDER_TEMPLATE_ID_FIELD = "id";

    /// <summary>
    /// Folder field for folder templates.
    /// </summary>
    public static readonly string FOLDER_TEMPLATE_FOLDER_FIELD = "folder";

    // IP structure related
    /// <summary>
    /// Metadata with the first letter capitalized.
    /// </summary>
    public static readonly string METADATA_WITH_FIRST_LETTER_CAPITAL = "Metadata";

    /// <summary>
    /// Metadata in lowercase.
    /// </summary>
    public static readonly string METADATA = "metadata";

    /// <summary>
    /// Folder path for metadata.
    /// </summary>
    public static readonly string METADATA_FOLDER = METADATA + METS_PATH_SEPARATOR;

    /// <summary>
    /// Descriptive metadata.
    /// </summary>
    public static readonly string DESCRIPTIVE = "descriptive";

    /// <summary>
    /// Folder path for descriptive metadata.
    /// </summary>
    public static readonly string DESCRIPTIVE_FOLDER = METADATA_FOLDER + DESCRIPTIVE + METS_PATH_SEPARATOR;

    /// <summary>
    /// Preservation metadata.
    /// </summary>
    public static readonly string PRESERVATION = "preservation";

    /// <summary>
    /// Folder path for preservation metadata.
    /// </summary>
    public static readonly string PRESERVATION_FOLDER = METADATA_FOLDER + PRESERVATION + METS_PATH_SEPARATOR;

    /// <summary>
    /// Other metadata with the first letter capitalized.
    /// </summary>
    public static readonly string OTHER_WITH_FIRST_LETTER_CAPITAL = "Other";

    /// <summary>
    /// Other metadata in lowercase.
    /// </summary>
    public static readonly string OTHER = "other";

    /// <summary>
    /// Folder path for other metadata.
    /// </summary>
    public static readonly string OTHER_FOLDER = METADATA_FOLDER + OTHER + METS_PATH_SEPARATOR;

    /// <summary>
    /// Representations with the first letter capitalized.
    /// </summary>
    public static readonly string REPRESENTATIONS_WITH_FIRST_LETTER_CAPITAL = "Representations";

    /// <summary>
    /// Representations in lowercase.
    /// </summary>
    public static readonly string REPRESENTATIONS = "representations";

    /// <summary>
    /// Folder path for representations.
    /// </summary>
    public static readonly string REPRESENTATIONS_FOLDER = REPRESENTATIONS + METS_PATH_SEPARATOR;

    /// <summary>
    /// Data with the first letter capitalized.
    /// </summary>
    public static readonly string DATA_WITH_FIRST_LETTER_CAPITAL = "Data";

    /// <summary>
    /// Data in lowercase.
    /// </summary>
    public static readonly string DATA = "data";

    /// <summary>
    /// Folder path for data.
    /// </summary>
    public static readonly string DATA_FOLDER = DATA + METS_PATH_SEPARATOR;

    /// <summary>
    /// Schemas with the first letter capitalized.
    /// </summary>
    public static readonly string SCHEMAS_WITH_FIRST_LETTER_CAPITAL = "Schemas";

    /// <summary>
    /// Schemas in lowercase.
    /// </summary>
    public static readonly string SCHEMAS = "schemas";

    /// <summary>
    /// Folder path for schemas.
    /// </summary>
    public static readonly string SCHEMAS_FOLDER = SCHEMAS + METS_PATH_SEPARATOR;

    /// <summary>
    /// Documentation with the first letter capitalized.
    /// </summary>
    public static readonly string DOCUMENTATION_WITH_FIRST_LETTER_CAPITAL = "Documentation";

    /// <summary>
    /// Documentation in lowercase.
    /// </summary>
    public static readonly string DOCUMENTATION = "documentation";

    /// <summary>
    /// Folder path for documentation.
    /// </summary>
    public static readonly string DOCUMENTATION_FOLDER = DOCUMENTATION + METS_PATH_SEPARATOR;

    // misc
    /// <summary>
    /// The separator used in ZIP paths.
    /// </summary>
    public static readonly string ZIP_PATH_SEPARATOR = "/";

    /// <summary>
    /// The MD5 checksum algorithm identifier.
    /// </summary>
    public static readonly string CHECKSUM_MD5_ALGORITHM = "MD5";

    /// <summary>
    /// The SHA-1 checksum algorithm identifier.
    /// </summary>
    public static readonly string CHECKSUM_SHA_1_ALGORITHM = "SHA1";

    /// <summary>
    /// The SHA-256 checksum algorithm identifier.
    /// </summary>
    public static readonly string CHECKSUM_SHA_256_ALGORITHM = "SHA-256";

    /// <summary>
    /// The default checksum algorithm identifier.
    /// </summary>
    public static readonly string CHECKSUM_ALGORITHM = CHECKSUM_SHA_256_ALGORITHM;

    // Common Specification (& E-ARK)
    /// <summary>
    /// Common Specification Structural Map identifier.
    /// </summary>
    public static readonly string COMMON_SPEC_STRUCTURAL_MAP = "CSIP";

    // SIP Specification
    /// <summary>
    /// URL for the SIP Specification profile.
    /// </summary>
    public static readonly string SIP_SPEC_PROFILE = "https://earksip.dilcis.eu/profile/E-ARK-SIP.xml";

    // EARK
    /// <summary>
    /// Structural map identifier for EARK SIP.
    /// </summary>
    public static readonly string EARK_SIP_STRUCTURAL_MAP = "RODA structural map";

    /// <summary>
    /// Division label for EARK SIP.
    /// </summary>
    public static readonly string EARK_SIP_DIV_LABEL = "RODA";

    /// <summary>
    /// Ancestors division label for EARK SIP.
    /// </summary>
    public static readonly string EARK_SIP_ANCESTORS_DIV_LABEL = "Ancestors";

    /// <summary>
    /// Indicates whether METS HREFs should be encoded and decoded.
    /// </summary>
    public static bool METS_ENCODE_AND_DECODE_HREF = true;

    // XML Schemas
    /// <summary>
    /// Path to the schemas directory in resources.
    /// </summary>
    public static readonly string SCHEMAS_PATH = "dotnet_eark_sip.src.Resources.Schemas.";

    /// <summary>
    /// Filename of the METS schema with version.
    /// </summary>
    public static readonly string SCHEMA_METS_FILENAME_WITH_VERSION = "mets1_12_1.xsd";

    /// <summary>
    /// Relative path to the METS schema from resources.
    /// </summary>
    public static readonly string SCHEMA_METS_RELATIVE_PATH_FROM_RESOURCES = SCHEMAS_PATH + SCHEMA_METS_FILENAME_WITH_VERSION;

    /// <summary>
    /// Filename of the XLINK schema.
    /// </summary>
    public static readonly string SCHEMA_XLINK_FILENAME = "xlink.xsd";

    /// <summary>
    /// Relative path to the XLINK schema from resources.
    /// </summary>
    public static readonly string SCHEMA_XLINK_RELATIVE_PATH_FROM_RESOURCES = SCHEMAS_PATH + SCHEMA_XLINK_FILENAME;

    /// <summary>
    /// URL of the XLINK schema.
    /// </summary>
    public static readonly string SCHEMA_XLINK_URL = "http://www.loc.gov/standards/xlink/xlink.xsd";

    /// <summary>
    /// Filename of the E-ARK CSIP schema.
    /// </summary>
    public static readonly string SCHEMA_EARK_CSIP_FILENAME = "DILCISExtensionMETS.xsd";

    /// <summary>
    /// Relative path to the E-ARK CSIP schema from resources.
    /// </summary>
    public static readonly string SCHEMA_EARK_CSIP_RELATIVE_PATH_FROM_RESOURCES = SCHEMAS_PATH + SCHEMA_EARK_CSIP_FILENAME;

    /// <summary>
    /// Filename of the E-ARK SIP schema.
    /// </summary>
    public static readonly string SCHEMA_EARK_SIP_FILENAME = "DILCISExtensionSIPMETS.xsd";

    /// <summary>
    /// Relative path to the E-ARK SIP schema from resources.
    /// </summary>
    public static readonly string SCHEMA_EARK_SIP_RELATIVE_PATH_FROM_RESOURCES = SCHEMAS_PATH + SCHEMA_EARK_SIP_FILENAME;

    /// <summary>
    /// The file extension for SIP files.
    /// </summary>
    public static readonly string SIP_FILE_EXTENSION = ".zip";

    /** Private empty constructor */
    private IPConstants()
    {
        // do nothing
    }

}
