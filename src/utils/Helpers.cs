public static class Utils {
  public static string GenerateRandomAndPrefixedUUID() {
    return METSEnums.ID_PREFIX + Guid.NewGuid().ToString().ToUpper();
  }
}