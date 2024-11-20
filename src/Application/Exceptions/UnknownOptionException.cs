namespace Engage.Application.Exceptions
{
    public class UnknownOptionException : Exception
    {
        public UnknownOptionException(string name)
            : base($"Option \"{name}\" does not exist.")
        {
        }
    }
}
