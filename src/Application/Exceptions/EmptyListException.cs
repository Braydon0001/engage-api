namespace Engage.Application.Exceptions
{
    public class EmptyListException : Exception
    {
        public EmptyListException(string name)
            : base($"List \"{name}\" is empty.")
        {
        }
    }
}
