/**
 * The contents of this file are subject to the license and copyright
 * detailed in the LICENSE file at the root of the source
 * tree and available online at
 *
 * https://github.com/keeps/commons-ip
 */
public class IPConstants {

  public static readonly string SYSTEM_LINE_SEP = Path.PathSeparator.ToString();

  // METS related
  public static readonly string METS_FILE_NAME = "METS";
  public static readonly string METS_FILE_EXTENSION = ".xml";
  public static readonly string METS_FILE = METS_FILE_NAME + METS_FILE_EXTENSION;
  public static readonly List<string> METS_FILE_PREFIXES_TO_ACCEPT = new List<string> { "file://./", "file:" };
  public static readonly string METS_TYPE_SIMPLE = "simple";
  public static readonly string METS_TYPE_PHYSICAL = "PHYSICAL";
  public static readonly string METS_PATH_SEPARATOR = "/";
  public static readonly string METS_REPRESENTATION_TYPE_PART_1 = "representation";

  public static readonly string METS_ORIGINAL = "original";
  public static readonly string METS_STATUS_CURRENT = "current";
  public static readonly string METS_GROUP_ID = "1";
  public static readonly string METS_TYPE_AGGREGATION = "aggregation";
  public static readonly string METS_TYPE_DOCUMENTATION = "documentation";
  public static readonly string METS_TYPE_RECORDGRP = "recordgrp";
  public static readonly string METS_LABEL_DOKU = "Doku";
  public static readonly string METS_LEVEL_OTHER = "otherlevel";
  public static readonly string METS_EAD_TYPE = "EAD";
  public static readonly string METS_EAD_VERSION = "2002";

  public static readonly string FOLDER_TEMPLATE_ID_FIELD = "id";
  public static readonly string FOLDER_TEMPLATE_FOLDER_FIELD = "folder";

  // IP structure related
  public static readonly string METADATA_WITH_FIRST_LETTER_CAPITAL = "Metadata";
  public static readonly string METADATA = "metadata";
  public static readonly string METADATA_FOLDER = METADATA + METS_PATH_SEPARATOR;
  public static readonly string DESCRIPTIVE = "descriptive";
  public static readonly string DESCRIPTIVE_FOLDER = METADATA_FOLDER + DESCRIPTIVE + METS_PATH_SEPARATOR;
  public static readonly string PRESERVATION = "preservation";
  public static readonly string PRESERVATION_FOLDER = METADATA_FOLDER + PRESERVATION + METS_PATH_SEPARATOR;
  public static readonly string OTHER_WITH_FIRST_LETTER_CAPITAL = "Other";
  public static readonly string OTHER = "other";
  public static readonly string OTHER_FOLDER = METADATA_FOLDER + OTHER + METS_PATH_SEPARATOR;
  public static readonly string REPRESENTATIONS_WITH_FIRST_LETTER_CAPITAL = "Representations";
  public static readonly string REPRESENTATIONS = "representations";
  public static readonly string REPRESENTATIONS_FOLDER = REPRESENTATIONS + METS_PATH_SEPARATOR;
  public static readonly string DATA_WITH_FIRST_LETTER_CAPITAL = "Data";
  public static readonly string DATA = "data";
  public static readonly string DATA_FOLDER = DATA + METS_PATH_SEPARATOR;
  public static readonly string SCHEMAS_WITH_FIRST_LETTER_CAPITAL = "Schemas";
  public static readonly string SCHEMAS = "schemas";
  public static readonly string SCHEMAS_FOLDER = SCHEMAS + METS_PATH_SEPARATOR;
  public static readonly string DOCUMENTATION_WITH_FIRST_LETTER_CAPITAL = "Documentation";
  public static readonly string DOCUMENTATION = "documentation";
  public static readonly string DOCUMENTATION_FOLDER = DOCUMENTATION + METS_PATH_SEPARATOR;
  public static readonly string SUBMISSION = "submission";
  public static readonly string SUBMISSION_FOLDER = SUBMISSION + METS_PATH_SEPARATOR;

  // misc
  public static readonly string ZIP_PATH_SEPARATOR = "/";
  public static readonly string CHECKSUM_MD5_ALGORITHM = "MD5";
  public static readonly string CHECKSUM_SHA_1_ALGORITHM = "SHA1";
  public static readonly string CHECKSUM_SHA_256_ALGORITHM = "SHA-256";
  public static readonly string CHECKSUM_ALGORITHM = CHECKSUM_SHA_256_ALGORITHM;

  // Common Specification (& E-ARK)
  public static readonly string COMMON_SPEC_STRUCTURAL_MAP = "CSIP";

  // SIP Specification
  public static readonly string SIP_SPEC_PROFILE = "https://earksip.dilcis.eu/profile/E-ARK-SIP.xml";

  // EARK
  public static readonly string EARK_SIP_STRUCTURAL_MAP = "EARK SIP structural map";
  public static readonly string EARK_SIP_DIV_LABEL = "EARK SIP";
  public static readonly string EARK_SIP_ANCESTORS_DIV_LABEL = "Ancestors";

  // Bagit
  public static readonly string BAGIT_PARENT = "parent";
  public static readonly string BAGIT_ID = "id";
  public static readonly string BAGIT_TITLE = "title";
  public static readonly string BAGIT_LEVEL = "level";
  public static readonly string BAGIT_ITEM_LEVEL = "item";
  public static readonly string BAGIT_DATA_FOLDER = "data";
  public static readonly string BAGIT_NAME = "name";
  public static readonly string BAGIT_FIELD = "field";
  public static readonly string BAGIT_METADATA = "metadata";
  public static readonly string BAGIT_VENDOR = "vendor";
  public static readonly string BAGIT_VENDOR_COMMONS_IP = "commons-ip";

  // Hungarian
  public static readonly string CONTENT_FOLDER = "content";
  public static readonly string HEADER_FOLDER = "header";
  public static readonly string METADATA_FILE = "metadata.xml";
  public static readonly string HUNGARIAN_METADATA_FILE = HEADER_FOLDER + "/" + METADATA_FILE;
  public static readonly string HUNGARIAN_DOCUMENTATION_TAG = "documentation";

  public static bool METS_ENCODE_AND_DECODE_HREF = true;

  // XML SChemas
  public static readonly string SCHEMA_METS_FILENAME_WITH_VERSION = "mets1_12_1.xsd";
  public static readonly string SCHEMA_METS_RELATIVE_PATH_FROM_RESOURCES = METS_PATH_SEPARATOR + SCHEMAS + "2"
    + METS_PATH_SEPARATOR + SCHEMA_METS_FILENAME_WITH_VERSION;
  public static readonly string SCHEMA_XLINK_FILENAME = "xlink.xsd";
  public static readonly string SCHEMA_XLINK_RELATIVE_PATH_FROM_RESOURCES = METS_PATH_SEPARATOR + SCHEMAS + "2"
    + METS_PATH_SEPARATOR + SCHEMA_XLINK_FILENAME;
  public static readonly string SCHEMA_XLINK_URL = "http://www.loc.gov/standards/xlink/xlink.xsd";
  public static readonly string SCHEMA_EARK_CSIP_FILENAME = "DILCISExtensionMETS.xsd";
  public static readonly string SCHEMA_EARK_CSIP_RELATIVE_PATH_FROM_RESOURCES = METS_PATH_SEPARATOR + SCHEMAS + "2"
    + METS_PATH_SEPARATOR + SCHEMA_EARK_CSIP_FILENAME;
  public static readonly string SCHEMA_EARK_SIP_FILENAME = "DILCISExtensionSIPMETS.xsd";
  public static readonly string SCHEMA_EARK_SIP_RELATIVE_PATH_FROM_RESOURCES = METS_PATH_SEPARATOR + SCHEMAS + "2"
    + METS_PATH_SEPARATOR + SCHEMA_EARK_SIP_FILENAME;

  public static readonly string SIP_FILE_EXTENSION = ".zip";

  /** Private empty constructor */
  private IPConstants() {
    // do nothing
  }

}
