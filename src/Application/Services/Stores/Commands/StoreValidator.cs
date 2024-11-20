namespace Engage.Application.Services.Stores.Commands;

public class StoreValidator<T> : AbstractValidator<T> where T : StoreCommand
{
    public StoreValidator()
    {
        RuleFor(x => x.StoreClusterId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreFormatId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreLSMId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.PrimaryLocationId).GreaterThan(0);
        RuleFor(x => x.PrimaryContactId).GreaterThan(0);
        RuleFor(x => x.Code).MaximumLength(30).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.CatManStoreCode).MaximumLength(30);
        RuleFor(x => x.StoreImageUrl).MaximumLength(300);
        RuleForEach(x => x.StoreDepartmentIds).GreaterThan(0);
        RuleForEach(x => x.StoreConceptIds).GreaterThan(0);
    }
}

public class CreateStoreValidator : StoreValidator<CreateStoreCommand>
{
}

public class UpdateStoreValidator : StoreValidator<UpdateStoreCommand>
{
    public UpdateStoreValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}