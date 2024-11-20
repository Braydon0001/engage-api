namespace Engage.Application.Services.EngageGroups.Commands;

public class EngageGroupValidator<T> : AbstractValidator<T> where T : EngageGroupCommand
{
    public EngageGroupValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}

public class CreateEngageGroupValidator : EngageGroupValidator<CreateEngageGroupCommand>
{
    public CreateEngageGroupValidator()
    {
    }
}

public class UpdateEngageGroupValidator : EngageGroupValidator<UpdateEngageGroupCommand>
{
    public UpdateEngageGroupValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
