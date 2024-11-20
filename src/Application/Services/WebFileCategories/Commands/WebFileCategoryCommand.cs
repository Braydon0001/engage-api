// auto-generated
namespace Engage.Application.Services.WebFileCategories.Commands;

public class WebFileCategoryCommand
{
    public int WebFileGroupId { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public int Order { get; set; }
}

public class WebFileCategoryValidator<T> : AbstractValidator<T> where T : WebFileCategoryCommand
{
    public WebFileCategoryValidator()
    {
        RuleFor(e => e.WebFileGroupId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(30);
        RuleFor(e => e.DisplayName).NotEmpty().MaximumLength(100);
        RuleFor(e => e.Order).NotEmpty().GreaterThan(0);
    }
}