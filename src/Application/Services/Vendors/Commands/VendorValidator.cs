namespace Engage.Application.Services.Vendors.Commands;

public class VendorValidator<T> : AbstractValidator<T> where T : VendorCommand
{
    public VendorValidator()
    {
        RuleFor(x => x.DistributionCenterId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.AccountNumber).MaximumLength(30);
    }
}

public class CreateVendorValidator : VendorValidator<CreateVendorCommand>
{
    public CreateVendorValidator()
    {       
    }
}

public class UpdateVendorValidator : VendorValidator<UpdateVendorCommand>
{
    public UpdateVendorValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
