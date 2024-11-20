namespace Engage.Application.Services.EngageVariantProducts.Commands;

public class EngageVariantProductValidator<T> : AbstractValidator<T> where T : EngageVariantProductCommand
{
    public EngageVariantProductValidator()
    {
        RuleFor(x => x.UnitTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(220).NotEmpty();
        RuleFor(x => x.Code).MaximumLength(30).NotEmpty();
        RuleFor(x => x.Size).GreaterThan(0).NotEmpty();
        RuleFor(x => x.PackSize).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EANNumber).MaximumLength(20);
    }
}

public class CreateEngageVariantProductValidator : EngageVariantProductValidator<CreateEngageVariantProductCommand>
{
    public CreateEngageVariantProductValidator()
    {
        RuleFor(x => x.EngageMasterProductId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEngageVariantProductValidator : EngageVariantProductValidator<UpdateEngageVariantProductCommand>
{
    public UpdateEngageVariantProductValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
