namespace Engage.Application.Services.TrainingPeriods.Commands;

class TrainingPeriodValidator<T> : AbstractValidator<T> where T : TrainingPeriodCommand
{
    public TrainingPeriodValidator()
    {
        RuleFor(x => x.TrainingYearId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
    }

    public class CreateTrainingPeriodValidator : TrainingPeriodValidator<CreateTrainingPeriodCommand>
    {
        public CreateTrainingPeriodValidator()
        {

        }
    }

    public class UpdateTrainingPeriodValidator : TrainingPeriodValidator<UpdateTrainingPeriodCommand>
    {
        public UpdateTrainingPeriodValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
