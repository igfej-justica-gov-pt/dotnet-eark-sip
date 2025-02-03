public class FolderWriteStrategyFactory : WriteStrategyFactory {
  protected override IWriteStrategy CreateWriteStrategy()
  {
    return new FolderWriteStrategy();
  }
}