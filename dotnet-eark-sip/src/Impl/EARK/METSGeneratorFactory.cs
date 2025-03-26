/// <summary>
/// Factory class for creating METS generator instances.
/// </summary>
public class METSGeneratorFactory {
  /// <summary>
  /// Initializes a new instance of the <see cref="METSGeneratorFactory"/> class.
  /// </summary>
  public METSGeneratorFactory() {}

  /// <summary>
  /// Gets the appropriate METS generator instance based on the specified version.
  /// </summary>
  /// <param name="version">The version of the METS generator to create.</param>
  /// <returns>An instance of <see cref="EARKMETSCreator"/> for the specified version.</returns>
  public EARKMETSCreator GetGenerator(string version) {
    if (version == "2.0.4") return new EARKMETSCreator204();
    return new EARKMETSCreator210();
  }
}