namespace Engage.Application.Services.SupplierRegions.Commands;

public class SupplierRegionValidator<T> : AbstractValidator<T> where T : SupplierRegionCommand
{
    public SupplierRegionValidator()
    {
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
    }
}

public class CreateSupplierRegionValidator : SupplierRegionValidator<CreateSupplierRegionCommand>
{
    public CreateSupplierRegionValidator()
    {
        RuleFor(e => e.SupplierId).NotEmpty().GreaterThan(1);
    }
}

public class UpdateSupplierRegionValidator : SupplierRegionValidator<UpdateSupplierRegionCommand>
{
    public UpdateSupplierRegionValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
