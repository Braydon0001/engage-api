namespace Engage.Application.Services.Targetings;

public class TargetingValidator<T> : AbstractValidator<T> where T : TargetingCommand
{
    public TargetingValidator()
    {
        RuleFor(e => e.Criteria).NotEmpty();
    }
}

class CreateTargetingValidator : TargetingValidator<CreateTargetingCommand>
{

}

class UpdateTargetingValidator : TargetingValidator<UpdateTargetingCommand>
{
    public UpdateTargetingValidator()
    {
        RuleFor(e => e.Id).GreaterThan(0).NotEmpty();
    }
}
