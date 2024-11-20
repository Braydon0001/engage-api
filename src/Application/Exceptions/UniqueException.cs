namespace Engage.Application.Exceptions;

public class UniqueException : Exception
{
    public UniqueException(string message)
        : base(message)
    {
    }
}
