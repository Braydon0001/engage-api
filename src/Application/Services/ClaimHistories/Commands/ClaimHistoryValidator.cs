namespace Engage.Application.Services.ClaimHistories.Commands;

public class CreateClaimHistoryValidator : AbstractValidator<CreateClaimHistoryCommand>
{
    public CreateClaimHistoryValidator()
    {
        RuleFor(x => x.Claim).NotNull();
        RuleFor(x => x.Claim.ClaimId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Claim.ClaimStatusId).GreaterThan(0);
        RuleFor(x => x.Claim.ClaimRejectedReasonId).GreaterThan(0);
        RuleFor(x => x.Claim.ClaimPendingReasonId).GreaterThan(0);
    }
}  
