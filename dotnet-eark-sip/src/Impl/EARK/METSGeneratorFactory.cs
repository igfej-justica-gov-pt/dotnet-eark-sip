public class METSGeneratorFactory {
  public METSGeneratorFactory() {}

  public EARKMETSCreator GetGenerator(string version) {
    if (version == "2.0.4") return new EARKMETSCreator204();
    return new EARKMETSCreator210();
  }
}