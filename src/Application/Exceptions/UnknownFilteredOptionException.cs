namespace Engage.Application.Exceptions
{
    public class UnknownFilteredOptionException : Exception
    {
        public UnknownFilteredOptionException(string name)
            : base($"Filtered Option \"{name}\" does not exist.")
        {
        }
    }
}
