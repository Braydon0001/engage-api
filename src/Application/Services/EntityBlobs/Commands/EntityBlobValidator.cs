using Engage.Application.Services.ClaimBlobs.Commands;

namespace Engage.Application.Services.EntityBlobs.Commands;

public class EntityBlobValidator<T> : AbstractValidator<T> where T : EntityBlobCommand
{
    public EntityBlobValidator()
    {
        RuleFor(x => x.FileName).MaximumLength(2000).NotEmpty();
        RuleFor(x => x.Url).MaximumLength(2000).NotEmpty();

    }
}

public class CreateClaimBlobValidator : EntityBlobValidator<CreateClaimBlobCommand>
{
    public CreateClaimBlobValidator()
    {
        RuleFor(x => x.ClaimId).GreaterThan(0).NotEmpty();
    }
}
