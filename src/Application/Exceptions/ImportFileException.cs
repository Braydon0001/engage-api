namespace Engage.Application.Exceptions
{
    public class ImportFileException: Exception
    {
        public ImportFileException()
        {
        }

        public ImportFileException(string message) : base(message)
        {
        }

        public ImportFileException(string message, Exception innerException) : base(message, innerException)
        {
        }        
    }
}
