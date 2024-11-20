namespace Engage.Application.Exceptions;

internal class PaginationException : Exception
{
 
    public PaginationException(string message) : base(message)
    {
    }    
}
