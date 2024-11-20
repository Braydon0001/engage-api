namespace Engage.Application.Services.EngageMasterProducts.Commands;

public class EngageMasterProductValidator<T> : AbstractValidator<T> where T : EngageMasterProductCommand
{
    public EngageMasterProductValidator()
    {
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ProductClassificationId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageDepartmentId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageSubCategoryId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageBrandId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.VatId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Code).MaximumLength(30).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(220).NotEmpty();
        RuleForEach(x => x.EngageTagIds).GreaterThan(0);
    }
}
public class CreateEngageMasterProductValidator : EngageMasterProductValidator<CreateEngageMasterProductCommand>
{
}

public class UpdateEngageMasterProductValidator : EngageMasterProductValidator<UpdateEngageMasterProductCommand>
{
    public UpdateEngageMasterProductValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
