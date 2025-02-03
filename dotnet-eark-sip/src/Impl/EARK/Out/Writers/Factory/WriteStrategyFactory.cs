public abstract class WriteStrategyFactory {
  public IWriteStrategy Create(string buildPath) {
    IWriteStrategy strategy = CreateWriteStrategy();
    strategy.Setup(buildPath);
    return strategy;
  }

  protected abstract IWriteStrategy CreateWriteStrategy();
}