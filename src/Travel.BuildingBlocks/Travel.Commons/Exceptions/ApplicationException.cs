namespace Travel.Commons.Exceptions;

public class ApplicationException : Exception
{
	public ApplicationException(string? message): base(message) { }
    public ApplicationException(string? message, Exception? exception) : base(message, exception) { }
    protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

