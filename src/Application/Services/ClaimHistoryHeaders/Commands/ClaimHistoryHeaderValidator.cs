namespace Engage.Application.Services.ClaimBatches.Commands;

public class CreateClaimHistoryHeaderValidator : AbstractValidator<CreateClaimHistoryHeaderCommand>
{
    public CreateClaimHistoryHeaderValidator()
    {
        RuleFor(x => x.ClaimStatusId).GreaterThan(0);
        RuleFor(x => x.ClaimSupplierStatusId).GreaterThan(0);
        RuleFor(x => x.ClaimStatusId).NotEmpty().When(x => !x.ClaimSupplierStatusId.HasValue);
        RuleFor(x => x.ClaimClassificationId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
    }
}  
