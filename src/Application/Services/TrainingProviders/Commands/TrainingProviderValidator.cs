namespace Engage.Application.Services.TrainingProviders.Commands;

public class TrainingProviderValidator<T> : AbstractValidator<T> where T : TrainingProviderCommand
{
    public TrainingProviderValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
    }
}

public class CreateTrainingProviderValidator : TrainingProviderValidator<CreateTrainingProviderCommand>
{
    public CreateTrainingProviderValidator()
    {
    }
}

public class UpdateTrainingProviderValidator : TrainingProviderValidator<UpdateTrainingProviderCommand>
{
    public UpdateTrainingProviderValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
