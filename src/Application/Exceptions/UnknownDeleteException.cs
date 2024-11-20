namespace Engage.Application.Exceptions
{
    public class UnknownDeleteException : Exception
    {
        public UnknownDeleteException(string name)
            : base($"Entity \"{name}\" can't be deleted or disabled because it does not exist.")
        {
        }
    }
}
