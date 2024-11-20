namespace Engage.Application.Services.DCProducts.Commands;

public class DCProductValidator<T> : AbstractValidator<T> where T : DCProductCommand
{
    public DCProductValidator()
    {
        RuleFor(x => x.VendorId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ManufacturerId).GreaterThan(0);
        RuleFor(x => x.ProductClassId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.UnitTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ProductActiveStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ProductStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ProductWarehouseStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ProductSubWarehouseId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(220).NotEmpty();
        RuleFor(x => x.Code).MaximumLength(30).NotEmpty();
        RuleFor(x => x.Size).GreaterThan(0).NotEmpty();
        RuleFor(x => x.PackSize).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EANNumber).MaximumLength(20);
        RuleFor(x => x.SubWarehouse).MaximumLength(20);
    }
}

public class CreateDCProductValidator : DCProductValidator<CreateDCProductCommand>
{
    public CreateDCProductValidator()
    {
        RuleFor(x => x.EngageVariantProductId).GreaterThan(0).NotEmpty();
    }

}

public class UpdateDCProductValidator : DCProductValidator<UpdateDCProductCommand>
{
    public UpdateDCProductValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
