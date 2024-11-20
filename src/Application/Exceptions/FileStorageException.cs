namespace Engage.Application.Exceptions;

public class FileStorageException : Exception
{
    public FileStorageException()
    {
    }

    public FileStorageException(string message) : base(message)
    {
    }

    public FileStorageException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
