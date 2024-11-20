namespace Engage.Application.Services.ProductAnalysisDivisions.Commands;

public class ProductAnalysisDivisionValidator<T> : AbstractValidator<T> where T : ProductAnalysisDivisionCommand
{
    public ProductAnalysisDivisionValidator()
    {
        RuleFor(e => e.Name).MaximumLength(100).NotEmpty();
        RuleFor(e => e.Description).MaximumLength(100);
    }
}