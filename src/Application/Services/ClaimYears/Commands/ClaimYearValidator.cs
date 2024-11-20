namespace Engage.Application.Services.ClaimYears.Commands;

class ClaimYearValidator<T> : AbstractValidator<T> where T : ClaimYearCommand
{
    public ClaimYearValidator()
    {
        RuleFor(x => x.Name).MaximumLength(20).NotEmpty();
    }

    public class CreateClaimYearValidator : ClaimYearValidator<CreateClaimYearCommand>
    {
        public CreateClaimYearValidator()
        {

        }
    }

    public class UpdateClaimYearValidator : ClaimYearValidator<UpdateClaimYearCommand>
    {
        public UpdateClaimYearValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
