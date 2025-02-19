public static class ExitCodes {
  /**
   * Exit code success.
   */
  public static int EXIT_CODE_OK = 0;

  /**
   * Exit code error.
   */
  public static int EXIT_CODE_ERROR = 1;

  /**
   * Exit code when missing args to execute the CLI.
   */
  public static int EXIT_CODE_CREATE_MISSING_ARGS = 2;
  /**
   * Exit code when missing args to execute the CLI.
   */
  public static int EXIT_CODE_CREATE_INVALID_ARGS = 2;

  /**
   * Exit code when fails to create the SIP.
   */
  public static int EXIT_CODE_CREATE_CANNOT_SIP = 3;

  /**
   * Exit code when the given paths are invalid.
   */
  public static int EXIT_CODE_CREATE_INVALID_PATHS = 4;
  /**
   * Exit code when can't create the directory.
   */
  public static int EXIT_CODE_CREATE_DIRECTORY_FAILS = 4;
  /**
   * Exit code when the date format is invalid.
   */

  public static int EXIT_CODE_INVALID_DATE_FORMAT = 5;
}
