namespace Engage.Application.Services.Shared.AssignCommands;

public class BaseAssignCommandValidator<T> : AbstractValidator<T> where T : AssignCommand
{
    public BaseAssignCommandValidator()
    {
        RuleFor(e => e.Mapping).NotEmpty();
        RuleFor(e => e.ToId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.AssignedId).GreaterThan(0).NotEmpty();
    }
}

public class AssignCommandValidator<T> : BaseAssignCommandValidator<AssignCommand>
{
}

public class UnassignCommandValidator<T> : BaseAssignCommandValidator<UnassignCommand>
{
}
