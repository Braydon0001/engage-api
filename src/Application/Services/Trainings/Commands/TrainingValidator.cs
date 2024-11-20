namespace Engage.Application.Services.Trainings.Commands;

public class TrainingValidator<T> : AbstractValidator<T> where T : TrainingCommand
{
    public TrainingValidator()
    {
        RuleFor(x => x.TrainingTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.Note).MaximumLength(300);
        RuleFor(x=> x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).NotEmpty();
    }
}

public class CreateTrainingValidator : TrainingValidator<CreateTrainingCommand>
{
    public CreateTrainingValidator()
    {
    }
}

public class UpdateTrainingValidator : TrainingValidator<UpdateTrainingCommand>
{
    public UpdateTrainingValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
