namespace Engage.Application.Services.EngageSubCategories.Commands;

public class EngageSubCategoryValidator<T> : AbstractValidator<T> where T : EngageSubCategoryCommand
{
    public EngageSubCategoryValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}

public class CreateEngageSubCategoryValidator : EngageSubCategoryValidator<CreateEngageSubCategoryCommand>
{
    public CreateEngageSubCategoryValidator()
    {
    }
}

public class UpdateEngageSubCategoryValidator : EngageSubCategoryValidator<UpdateEngageSubCategoryCommand>
{
    public UpdateEngageSubCategoryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
