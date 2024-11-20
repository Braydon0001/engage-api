namespace Engage.Application.Services.ClaimTypes.Commands;

public class ClaimTypeValidator<T> : AbstractValidator<T> where T : ClaimTypeCommand
{
    public ClaimTypeValidator()
    {
        RuleFor(x => x.VatId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.SupplierId).GreaterThan(0);
    }

    public class CreateClaimTypeValidator : ClaimTypeValidator<CreateClaimTypeCommand>
    {
        public CreateClaimTypeValidator()
        {
        }
    }

    public class UpdateClaimTypeValidator : ClaimTypeValidator<UpdateClaimTypeCommand>
    {
        public UpdateClaimTypeValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
