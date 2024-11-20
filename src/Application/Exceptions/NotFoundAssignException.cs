namespace Engage.Application.Exceptions
{
    public class NotFoundAssignException : Exception
    {
        public NotFoundAssignException(string mapping, int toId, int assignedId)
            : base($"Assign mapping \"{mapping}\" (toId: {toId} assignedId: {assignedId}) was not found.")
        {
        }
    }
}