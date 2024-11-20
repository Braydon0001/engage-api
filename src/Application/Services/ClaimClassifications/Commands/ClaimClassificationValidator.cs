namespace Engage.Application.Services.ClaimClassifications.Commands;

public class ClaimClassificationValidator<T> : AbstractValidator<T> where T : ClaimClassificationCommand
{
    public ClaimClassificationValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.ClaimTypeId).GreaterThan(0);
        RuleFor(x => x.SupplierId).GreaterThan(0);
    }
}

public class CreateClaimClassificationValidator : ClaimClassificationValidator<CreateClaimClassificationCommand>
{
    public CreateClaimClassificationValidator()
    {
    }
}

public class UpdateClaimClassificationValidator : ClaimClassificationValidator<UpdateClaimClassificationCommand>
{
    public UpdateClaimClassificationValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
