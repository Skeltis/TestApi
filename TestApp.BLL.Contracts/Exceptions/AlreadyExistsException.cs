using System.Runtime.Serialization;

namespace TestApp.BLL.Contracts.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException() : base() { }
    public AlreadyExistsException(string? message) : base(message) { }
    public AlreadyExistsException(string? message, Exception? innerException) : base(message, innerException) { }
    public AlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

