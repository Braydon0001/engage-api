namespace Engage.Application.Services.WebFiles.Commands;

public class WebFileValidator<T> : AbstractValidator<T> where T : WebFileCommand
{
    public WebFileValidator()
    {
        RuleFor(e => e.WebFileCategoryId).GreaterThan(0);
        RuleFor(e => e.FileTypeId).GreaterThan(0);
        RuleFor(e => e.TargetStrategyId).GreaterThan(0);
        RuleFor(e => e.EmployeeId).GreaterThan(0);
        RuleFor(e => e.StoreId).GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
        RuleFor(e => e.DisplayName).NotEmpty().MaximumLength(100);
        RuleFor(e => e.StartDate).NotNull();
    }
}