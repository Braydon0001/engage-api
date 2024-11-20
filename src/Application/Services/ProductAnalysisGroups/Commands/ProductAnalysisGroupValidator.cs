namespace Engage.Application.Services.ProductAnalysisGroups.Commands;

public class ProductAnalysisGroupValidator<T> : AbstractValidator<T> where T : ProductAnalysisGroupCommand
{
    public ProductAnalysisGroupValidator()
    {
        RuleFor(e => e.Name).MaximumLength(100).NotEmpty();
        RuleFor(e => e.Description).MaximumLength(100);
    }
}