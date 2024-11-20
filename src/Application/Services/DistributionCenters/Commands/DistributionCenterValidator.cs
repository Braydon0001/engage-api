namespace Engage.Application.Services.DistributionCenters.Commands;

public class DistributionCenterValidator<T> : AbstractValidator<T> where T : DistributionCenterCommand
{
    public DistributionCenterValidator()
    {
        RuleFor(x => x.Code).MaximumLength(20);
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleForEach(x => x.WarehouseIds).GreaterThan(0);
        RuleForEach(x => x.DepartmentIds).GreaterThan(0);
    }
}

public class CreateDistributionCenterValidator : DistributionCenterValidator<CreateDistributionCenterCommand>
{
}

public class UpdateDistributionCenterValidator : DistributionCenterValidator<UpdateDistributionCenterCommand>
{
    public UpdateDistributionCenterValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
