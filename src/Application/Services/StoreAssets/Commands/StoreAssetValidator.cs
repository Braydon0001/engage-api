namespace Engage.Application.Services.StoreAssets.Commands;

public class StoreAssetValidator<T> : AbstractValidator<T> where T : StoreAssetCommand
{
    public StoreAssetValidator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreAssetTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreAssetStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120);
        RuleFor(x => x.Description).MaximumLength(100);
        RuleFor(x => x.Note).MaximumLength(120);
        RuleFor(x => x.SerialNumber).MaximumLength(100).NotEmpty();
    }
}