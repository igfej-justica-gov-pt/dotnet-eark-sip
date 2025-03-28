/// <summary>
/// A factory class for creating write strategies.
/// </summary>
public abstract class WriteStrategyFactory
{
  /// <summary>
  /// Creates a write strategy and sets it up with the specified build path.
  /// </summary>
  /// <param name="buildPath">The path to set up the write strategy.</param>
  /// <returns>An instance of <see cref="IWriteStrategy"/>.</returns>
  public IWriteStrategy Create(string buildPath)
  {
    IWriteStrategy strategy = CreateWriteStrategy();
    strategy.Setup(buildPath);
    return strategy;
  }

  /// <summary>
  /// Creates an instance of a write strategy.
  /// </summary>
  /// <returns>An instance of <see cref="IWriteStrategy"/>.</returns>
  protected abstract IWriteStrategy CreateWriteStrategy();
}