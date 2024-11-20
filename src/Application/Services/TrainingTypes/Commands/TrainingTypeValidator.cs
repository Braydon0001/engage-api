namespace Engage.Application.Services.TrainingTypes.Commands;

public class TrainingTypeValidator<T> : AbstractValidator<T> where T : TrainingTypeCommand
{
    public TrainingTypeValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
    }
}

public class CreateTrainingTypeValidator : TrainingTypeValidator<CreateTrainingTypeCommand>
{
    public CreateTrainingTypeValidator()
    {
    }
}

public class UpdateTrainingTypeValidator : TrainingTypeValidator<UpdateTrainingTypeCommand>
{
    public UpdateTrainingTypeValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
