public class SIPBuilderException : Exception {
  public SIPBuilderException(string message) : base(message) {}
  public SIPBuilderException(string message, Exception inner) : base(message, inner) {}
}