public class Constants {
  public static readonly string SYSTEM_PATH_SEP = Path.PathSeparator.ToString();

  public static readonly string RESOURCES_PATH = "resources";
  public static readonly string CONTROLLED_VOCABULARIES_PATH = RESOURCES_PATH + SYSTEM_PATH_SEP + "controlledVocabularies";
  public static readonly string IANA_MEDIA_TYPES_PATH = CONTROLLED_VOCABULARIES_PATH + SYSTEM_PATH_SEP + "IANA_MEDIA_TYPES.txt";
}
