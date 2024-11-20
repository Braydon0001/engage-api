namespace Engage.Application.Services.Options.Commands;

public class OptionValidator<T> : AbstractValidator<T> where T : OptionCommand
{
    public OptionValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}

public class CreateOptionValidator : OptionValidator<CreateOptionCommand>
{
    public CreateOptionValidator()
    {
    }
}

public class UpdateOptionValidator : OptionValidator<UpdateOptionCommand>
{
    public UpdateOptionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}

public class DeleteOptionValidator : AbstractValidator<DeleteOptionCommand>
{
    public DeleteOptionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Option).NotEmpty();
    }
}
