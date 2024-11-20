namespace Engage.Application.Services.TrainingCategories.Commands;

public class TrainingCategoryValidator<T> : AbstractValidator<T> where T : TrainingCategoryCommand
{
    public TrainingCategoryValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
    }
}

public class CreateTrainingCategoryValidator : TrainingCategoryValidator<CreateTrainingCategoryCommand>
{
    public CreateTrainingCategoryValidator()
    {
    }
}

public class UpdateTrainingCategoryValidator : TrainingCategoryValidator<UpdateTrainingCategoryCommand>
{
    public UpdateTrainingCategoryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
