namespace Engage.Application.Services.AssetImages.Commands;

public class StoreAssetBlobValidator<T> : AbstractValidator<T> where T : StoreAssetBlobCommand
{
    public StoreAssetBlobValidator()
    {
        RuleFor(x => x.StoreAssetId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ImageUrl).MaximumLength(120).NotEmpty();
    }
}

public class StoreAssetBlobCreateValidator : StoreAssetBlobValidator<StoreAssetBlobCreateCommand>
{
    public StoreAssetBlobCreateValidator()
    {
    }
}

public class StoreAssetBlobUpdateValidator : StoreAssetBlobValidator<StoreAssetBlobUpdateCommand>
{
    public StoreAssetBlobUpdateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
