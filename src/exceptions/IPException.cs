public class IPException : Exception {
  public IPException(string message) : base(message) {}
  public IPException(string message, Exception inner) : base(message, inner) {}
}