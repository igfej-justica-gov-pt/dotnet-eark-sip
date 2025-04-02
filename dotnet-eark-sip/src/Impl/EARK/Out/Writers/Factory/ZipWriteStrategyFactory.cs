/// <summary>
/// Factory class for creating instances of ZipWriteStrategy.
/// </summary>
public class ZipWriteStrategyFactory : WriteStrategyFactory
{
    /// <summary>
    /// Creates an instance of the ZipWriteStrategy.
    /// </summary>
    /// <returns>An instance of IWriteStrategy implemented by ZipWriteStrategy.</returns>
    protected override IWriteStrategy CreateWriteStrategy()
    {
        return new ZipWriteStrategy();
    }
}