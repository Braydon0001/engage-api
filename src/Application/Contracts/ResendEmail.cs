namespace Engage.Application.Contracts;
public record ResendEmail() : BaseContract
{
    public int[] EmailIds { get; set; }
}
