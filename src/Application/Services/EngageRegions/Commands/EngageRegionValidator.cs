namespace Engage.Application.Services.EngageRegions.Commands;

public class EngageRegionValidator<T> : AbstractValidator<T> where T : EngageRegionCommand
{
    public EngageRegionValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateClaimRegionValidator : EngageRegionValidator<CreateEngageRegionCommand>
{
    public CreateClaimRegionValidator()
    {
    }
}

public class UpdateClaimRegionValidator : EngageRegionValidator<UpdateEngageRegionCommand>
{
    public UpdateClaimRegionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
