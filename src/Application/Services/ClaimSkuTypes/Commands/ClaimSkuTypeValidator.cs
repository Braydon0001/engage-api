namespace Engage.Application.Services.ClaimSkuTypes.Commands;

public class ClaimSkuTypeValidator<T> : AbstractValidator<T> where T : ClaimSkuTypeCommand
{
    public ClaimSkuTypeValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();

    }

    public class CreateClaimSkuTypeValidator : ClaimSkuTypeValidator<CreateClaimSkuTypeCommand>
    {
        public CreateClaimSkuTypeValidator()
        {
        }
    }

    public class UpdateClaimSkuTypeValidator : ClaimSkuTypeValidator<UpdateClaimSkuTypeCommand>
    {
        public UpdateClaimSkuTypeValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
