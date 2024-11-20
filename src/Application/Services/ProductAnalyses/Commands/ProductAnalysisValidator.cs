namespace Engage.Application.Services.ProductAnalyses.Commands;

public class ProductAnalysisValidator<T> : AbstractValidator<T> where T : ProductAnalysisCommand
{
    public ProductAnalysisValidator()
    {
        RuleFor(e => e.ProductAnalysisGroupId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.ProductAnalysisDivisionId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.EngageGroupId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.EngageSubGroupId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.EngageCategoryId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.EngageSubCategoryId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.DistributionCenterId).GreaterThan(0).NotEmpty();
        RuleFor(e => e.Supplier).MaximumLength(200);
        RuleFor(e => e.Vendor).MaximumLength(200);
        RuleFor(e => e.Manufacturer).MaximumLength(200);
        RuleFor(e => e.Product).MaximumLength(200);
        RuleFor(e => e.ProductDescription).MaximumLength(200);
        RuleFor(e => e.Size).MaximumLength(100);
        RuleFor(e => e.Key).MaximumLength(100);
        RuleFor(e => e.Barcode).MaximumLength(100);
        RuleFor(e => e.LedgerCode).MaximumLength(100);
        RuleFor(e => e.Listed).GreaterThanOrEqualTo(0);
        RuleFor(e => e.New).GreaterThanOrEqualTo(0);
        RuleFor(e => e.Sold).GreaterThanOrEqualTo(0);
    }
}