namespace Engage.Application.Services.EmployeeAssets.Commands;

public class EmployeeAssetValidator<T> : AbstractValidator<T> where T : EmployeeAssetCommand
{
    public EmployeeAssetValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeAssetTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeAssetBrandId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.AssetStatusId).GreaterThan(0).NotEmpty(); ;
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.MobileNumber).MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(100);
        RuleFor(x => x.Contract).MaximumLength(100);
        RuleFor(x => x.Sim).MaximumLength(100);
        RuleFor(x => x.Imei).MaximumLength(100);
        RuleFor(x => x.SerialNumber).MaximumLength(100);
        RuleFor(x => x.Note).MaximumLength(200);
    }
}