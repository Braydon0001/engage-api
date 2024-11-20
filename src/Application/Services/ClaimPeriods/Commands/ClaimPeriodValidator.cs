namespace Engage.Application.Services.ClaimPeriods.Commands;

class ClaimPeriodValidator<T> : AbstractValidator<T> where T : ClaimPeriodCommand
{
    public ClaimPeriodValidator()
    {
        RuleFor(x => x.ClaimYearId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
    }

    public class CreateClaimPeriodValidator : ClaimPeriodValidator<CreateClaimPeriodCommand>
    {
        public CreateClaimPeriodValidator()
        {

        }
    }

    public class UpdateClaimPeriodValidator : ClaimPeriodValidator<UpdateClaimPeriodCommand>
    {
        public UpdateClaimPeriodValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
