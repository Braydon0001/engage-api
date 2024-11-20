namespace Engage.Application.Services.VoucherDetails.Commands;

public class VoucherDetailValidator<T> : AbstractValidator<T> where T : VoucherDetailCommand
{
    public VoucherDetailValidator()
    {
        RuleFor(x => x.VoucherId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        RuleFor(x => x.VoucherNumber).MaximumLength(100).NotEmpty();
        //RuleFor(x => x.Note).MaximumLength(300);
    }
}

//public class CreateVoucherDetailValidator : VoucherDetailValidator<CreateVoucherDetailCommand>
//{
//    public CreateVoucherDetailValidator()
//    {
//    }
//}

//public class UpdateVoucherDetailValidator : VoucherDetailValidator<UpdateVoucherDetailCommand>
//{
//    public UpdateVoucherDetailValidator()
//    {
//        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
//    }
//}

//public class UpdateVoucherDetailQuantityTypeValidator : AbstractValidator<UpdateVoucherDetailQuantityTypeCommand>
//{
//    public UpdateVoucherDetailQuantityTypeValidator()
//    {
//        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
//        RuleFor(x => x.ClaimQuantityTypeId).GreaterThan(0).NotEmpty();
//    }
//}

//public class UpdateVoucherDetailQuantityValidator : AbstractValidator<UpdateVoucherDetailQuantityCommand>
//{
//    public UpdateVoucherDetailQuantityValidator()
//    {
//        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
//        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
//    }
//}

//public class UpdateVoucherDetailAmountValidator : AbstractValidator<UpdateVoucherDetailAmountCommand>
//{
//    public UpdateVoucherDetailAmountValidator()
//    {
//        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
//        RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
//        RuleFor(x => x.VatId).GreaterThan(0).NotEmpty();
//        RuleFor(x => x.ClaimVatId).GreaterThan(0);
//    }
//}

//public class UpdateVoucherDetailNoteValidator : AbstractValidator<UpdateVoucherDetailNoteCommand>
//{
//    public UpdateVoucherDetailNoteValidator()
//    {
//        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
//        RuleFor(x => x.Note).MaximumLength(300);
//    }
//}
