namespace Engage.Application.Exceptions
{
    public class BlobException : Exception
    {
        public BlobException()
        {
        }

        public BlobException(string message) : base(message)
        {
        }

        public BlobException(string message, Exception innerException) : base(message, innerException)
        {
        }        
    }
}
