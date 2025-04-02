/// <summary>
/// Represents an exception specific to IP-related errors.
/// </summary>
public class IPException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IPException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public IPException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="IPException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="inner">The exception that is the cause of the current exception.</param>
    public IPException(string message, Exception inner) : base(message, inner) { }
}