/// <summary>
/// Factory class for creating folder write strategies.
/// </summary>
public class FolderWriteStrategyFactory : WriteStrategyFactory
{
    /// <summary>
    /// Creates and returns an instance of the folder write strategy.
    /// </summary>
    /// <returns>An instance of <see cref="FolderWriteStrategy"/>.</returns>
    protected override IWriteStrategy CreateWriteStrategy()
    {
        return new FolderWriteStrategy();
    }
}