using Microsoft.Extensions.Logging;

public static class DefaultLogger {
  public static ILogger<T> Create<T>() {
    return LoggerFactory.Create(builder => {}).CreateLogger<T>();
  }

  public static ILogger Create(string categoryName) {
    return LoggerFactory.Create(builder => {}).CreateLogger(categoryName);
  }
}
