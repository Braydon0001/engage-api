namespace Engage.Application.Services.StoreConceptAttributeStoreAssets.Commands;

public class StoreConceptAttributeStoreAssetValidator<T> : AbstractValidator<T> where T : StoreConceptAttributeStoreAssetCommand
{
    public StoreConceptAttributeStoreAssetValidator()
    {
        RuleFor(x => x.StoreConceptAttributeId).NotEmpty();
        RuleFor(x => x.StoreAssetId).NotEmpty();
    }
}

public class StoreConceptAttributeStoreAssetCreateValidator : StoreConceptAttributeStoreAssetValidator<StoreConceptAttributeStoreAssetCreateCommand>
{
}

public class StoreConceptAttributeStoreAssetDeleteValidator : StoreConceptAttributeStoreAssetValidator<StoreConceptAttributeStoreAssetDeleteCommand>
{
}
