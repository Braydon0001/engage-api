namespace Engage.Application.Exceptions
{
    public class UnknownAssignMapException : Exception
    {
        public UnknownAssignMapException(string map)
            : base($"Assign map\"{map}\" does not exist.")
        {
        }
    }
}
