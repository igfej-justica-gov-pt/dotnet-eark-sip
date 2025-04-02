using Microsoft.Extensions.Logging;

/// <summary>
/// Provides methods to create loggers for specific types or categories.
/// </summary>
public static class DefaultLogger
{
    /// <summary>
    /// Creates a logger for the specified type.
    /// </summary>
    /// <typeparam name="T">The type for which the logger is created.</typeparam>
    /// <returns>An instance of <see cref="ILogger{T}"/>.</returns>
    public static ILogger<T> Create<T>()
    {
        return LoggerFactory.Create(builder => { }).CreateLogger<T>();
    }

    /// <summary>
    /// Creates a logger for the specified category name.
    /// </summary>
    /// <param name="categoryName">The category name for the logger.</param>
    /// <returns>An instance of <see cref="ILogger"/>.</returns>
    public static ILogger Create(string categoryName)
    {
        return LoggerFactory.Create(builder => { }).CreateLogger(categoryName);
    }
}
