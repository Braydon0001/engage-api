namespace Engage.Application.Services.Vouchers.Commands;

public class VoucherValidator<T> : AbstractValidator<T> where T : VoucherCommand
{
    public VoucherValidator()
    {
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Total).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Note).MaximumLength(220);
    }
}

public class CreateVoucherValidator : VoucherValidator<CreateVoucherCommand>
{
    public CreateVoucherValidator()
    {
    }
}

public class UpdateVoucherValidator : VoucherValidator<UpdateVoucherCommand>
{
    public UpdateVoucherValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
