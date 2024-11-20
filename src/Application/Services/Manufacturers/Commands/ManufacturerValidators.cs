namespace Engage.Application.Services.Manufacturers.Commands;

public class ManufacturerValidators<T> : AbstractValidator<T> where T : ManufacturerCommand
{
    public ManufacturerValidators()
    {
        RuleFor(e => e.EngageRegionId).NotEmpty().GreaterThan(1);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.AccountNumber).MaximumLength(30);
    }
}

public class CreateManufacturerValidator: ManufacturerValidators<CreateManufacturerCommand>
{
    public CreateManufacturerValidator()
    {
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(1);        
    }
}

public class UpdateManufacturerValidator: ManufacturerValidators<UpdateManufacturerCommand>
{
    public UpdateManufacturerValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(1);        
    }
}
