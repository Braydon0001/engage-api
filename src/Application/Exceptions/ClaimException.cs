namespace Engage.Application.Exceptions;

public class ClaimException : Exception
{
    public ClaimException(string message, int? claimId = null) 
        : base(claimId.HasValue ? $"Claim id: {claimId} ." :  message)
    {
    }
}
