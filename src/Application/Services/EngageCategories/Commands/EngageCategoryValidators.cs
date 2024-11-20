namespace Engage.Application.Services.EngageCategories.Commands;

public class EngageCategoryValidator<T> : AbstractValidator<T> where T : EngageCategoryCommand
{
    public EngageCategoryValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}

public class CreateEngageCategoryValidator : EngageCategoryValidator<CreateEngageCategoryCommand>
{
    public CreateEngageCategoryValidator()
    {
    }
}

public class UpdateEngageCategoryValidator : EngageCategoryValidator<UpdateEngageCategoryCommand>
{
    public UpdateEngageCategoryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
