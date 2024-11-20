namespace Engage.Application.Services.ClaimSkus.Commands
{
    public class ClaimSkuValidator<T> : AbstractValidator<T> where T : ClaimSkuCommand
    {
        public ClaimSkuValidator()
        {
            RuleFor(x => x.ClaimId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.ClaimSkuTypeId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.ClaimQuantityTypeId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.ClaimSkuStatusId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.VatAmount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.DCProductId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Note).MaximumLength(300);
        }
    }

    public class CreateClaimSkuValidator : ClaimSkuValidator<CreateClaimSkuCommand>
    {
        public CreateClaimSkuValidator()
        {
        }
    }

    public class UpdateClaimSkuValidator : ClaimSkuValidator<UpdateClaimSkuCommand>
    {
        public UpdateClaimSkuValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        }
    }

    public class UpdateClaimSkuQuantityTypeValidator : AbstractValidator<UpdateClaimSkuQuantityTypeCommand>
    {
        public UpdateClaimSkuQuantityTypeValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
            RuleFor(x => x.ClaimQuantityTypeId).GreaterThan(0).NotEmpty();
        }
    }

    public class UpdateClaimSkuQuantityValidator : AbstractValidator<UpdateClaimSkuQuantityCommand>
    {
        public UpdateClaimSkuQuantityValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        }
    }

    public class UpdateClaimSkuAmountValidator : AbstractValidator<UpdateClaimSkuAmountCommand>
    {
        public UpdateClaimSkuAmountValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.VatId).GreaterThan(0).NotEmpty();
            RuleFor(x => x.ClaimVatId).GreaterThan(0);
        }
    }

    public class UpdateClaimSkuNoteValidator : AbstractValidator<UpdateClaimSkuNoteCommand>
    {
        public UpdateClaimSkuNoteValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
            RuleFor(x => x.Note).MaximumLength(300);
        }
    }
}
