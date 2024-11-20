namespace Engage.Application.Services.EngageSubGroups.Commands;

public class EngageSubGroupValidator<T> : AbstractValidator<T> where T : EngageSubGroupCommand
{
    public EngageSubGroupValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
        RuleFor(e => e.StoreDepartmentId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.EngageDepartmentId).GreaterThan(0).NotEmpty();
        RuleForEach(e => e.SupplierIds).GreaterThan(0);
    }
}

public class CreateEngageSubGroupValidator : EngageSubGroupValidator<CreateEngageSubGroupCommand>
{
    public CreateEngageSubGroupValidator()
    {
    }
}

public class UpdateEngageSubGroupValidator : EngageSubGroupValidator<UpdateEngageSubGroupCommand>
{
    public UpdateEngageSubGroupValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
