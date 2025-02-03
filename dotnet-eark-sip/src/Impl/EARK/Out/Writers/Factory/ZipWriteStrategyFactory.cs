public class ZipWriteStrategyFactory : WriteStrategyFactory {
  protected override IWriteStrategy CreateWriteStrategy()
  {
    return new ZipWriteStrategy();
  }
}