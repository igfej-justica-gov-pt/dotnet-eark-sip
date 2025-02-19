public class InvalidArgumentsException : Exception {
  public InvalidArgumentsException(string message) : base(message) {}
  public InvalidArgumentsException(string message, Exception inner) : base(message, inner) {}
}