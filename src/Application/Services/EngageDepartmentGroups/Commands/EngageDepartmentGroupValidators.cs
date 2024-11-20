namespace Engage.Application.Services.EngageDepartmentGroups.Commands;

public class EngageDepartmentGroupValidator<T> : AbstractValidator<T> where T : EngageDepartmentGroupCommand
{
    public EngageDepartmentGroupValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}

public class EngageDepartmentGroupCreateValidator : EngageDepartmentGroupValidator<EngageDepartmentGroupCreateCommand>
{
    public EngageDepartmentGroupCreateValidator()
    {
    }
}

public class EngageDepartmentGroupUpdateValidator : EngageDepartmentGroupValidator<EngageDepartmentGroupUpdateCommand>
{
    public EngageDepartmentGroupUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
