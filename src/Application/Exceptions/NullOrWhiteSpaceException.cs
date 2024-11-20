namespace Engage.Application.Exceptions
{
    public class NullOrWhitespaceException : Exception
    {
        public NullOrWhitespaceException(string name)
            : base($"String \"{name}\" is null or whitespace.")
        {
        }
    }
}
