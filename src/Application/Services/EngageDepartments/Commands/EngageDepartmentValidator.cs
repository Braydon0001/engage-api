namespace Engage.Application.Services.EngageDepartments.Commands;

public class EngageDepartmentValidator<T> : AbstractValidator<T> where T : EngageDepartmentCommand
{
    public EngageDepartmentValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.EngageDepartmentGroupId).GreaterThan(0).NotEmpty();
    }
}

public class EngageDepartmentCreateValidator : EngageDepartmentValidator<EngageDepartmentCreateCommand>
{
    public EngageDepartmentCreateValidator()
    {
    }
}

public class EngageDepartmentUpdteValidator : EngageDepartmentValidator<EngageDepartmentUpdateCommand>
{
    public EngageDepartmentUpdteValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
