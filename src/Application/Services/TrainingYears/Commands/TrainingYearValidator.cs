namespace Engage.Application.Services.TrainingYears.Commands;

class TrainingYearValidator<T> : AbstractValidator<T> where T : TrainingYearCommand
{
    public TrainingYearValidator()
    {
        RuleFor(x => x.Name).MaximumLength(20).NotEmpty();
    }

    public class CreateTrainingYearValidator : TrainingYearValidator<CreateTrainingYearCommand>
    {
        public CreateTrainingYearValidator()
        {

        }
    }

    public class UpdateTrainingYearValidator : TrainingYearValidator<UpdateTrainingYearCommand>
    {
        public UpdateTrainingYearValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }
}
