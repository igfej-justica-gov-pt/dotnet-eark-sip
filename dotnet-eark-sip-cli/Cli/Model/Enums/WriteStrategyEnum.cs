public class WriteStrategyEnum {
  public static readonly WriteStrategyEnum ZIP = new("Zip");
  public static readonly WriteStrategyEnum FOLDER = new("Folder");

  public string WriteStrategy { get; }

  private WriteStrategyEnum(string strategy) {
    WriteStrategy = strategy;
  }

  public override string ToString() => WriteStrategy;
}