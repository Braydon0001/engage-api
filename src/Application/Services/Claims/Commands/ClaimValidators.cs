namespace Engage.Application.Services.Claims.Commands;

public class ClaimValidator<T> : AbstractValidator<T> where T : ClaimCommand
{
    public ClaimValidator()
    {
        RuleFor(x => x.ClientTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimClassificationId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.VatId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimAccountManagerId).GreaterThan(0);
        RuleFor(x => x.ClaimManagerId).GreaterThan(0);
        RuleFor(x => x.ClaimNumber).MaximumLength(100);
        RuleFor(x => x.ClaimDate).NotEmpty();
        RuleFor(x => x.ClaimReference).MaximumLength(220);
        RuleFor(x => x.Comment).MaximumLength(300);
    }
}

public class CreateClaimValidator : ClaimValidator<CreateClaimCommand>
{
    public CreateClaimValidator()
    {
    }
}

public class UpdateClaimValidator : ClaimValidator<UpdateClaimCommand>
{
    public UpdateClaimValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}

public class UpdateClaimDateValidator : AbstractValidator<UpdateClaimDateCommand>
{
    public UpdateClaimDateValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimDate).NotEmpty();
    }
}

public class UpdateClaimNumberValidator : AbstractValidator<UpdateClaimNumberCommand>
{
    public UpdateClaimNumberValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimNumber).MaximumLength(30).NotEmpty();
    }
}

public class UpdateClaimReferenceValidator : AbstractValidator<UpdateClaimReferenceCommand>
{
    public UpdateClaimReferenceValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimReference).MaximumLength(220).NotEmpty();
    }
}

public class UpdateClaimStatusValidator : AbstractValidator<UpdateClaimStatusCommand>
{
    public UpdateClaimStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimRejectedReasonId).GreaterThan(0);
        RuleFor(x => x.RejectedReason).MaximumLength(300);
    }
}

public class UpdateClaimSupplierStatusValidator : AbstractValidator<UpdateClaimSupplierStatusCommand>
{
    public UpdateClaimSupplierStatusValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimSupplierStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimPendingReasonId).GreaterThan(0);
        RuleFor(x => x.PendingReason).MaximumLength(300);
    }
}

public class BatchUpdateClaimStatusValidator : AbstractValidator<BatchUpdateClaimStatusCommand>
{
    public BatchUpdateClaimStatusValidator()
    {
        RuleForEach(x => x.ClaimIds).GreaterThan(0);
        RuleFor(x => x.ClaimStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimRejectedReasonId).GreaterThan(0);
        RuleFor(x => x.RejectedReason).MaximumLength(300);
    }
}

public class BatchUpdateClaimSupplierStatusValidator : AbstractValidator<BatchUpdateClaimSupplierStatusCommand>
{
    public BatchUpdateClaimSupplierStatusValidator()
    {
        RuleForEach(x => x.ClaimIds).GreaterThan(0);
        RuleFor(x => x.ClaimSupplierStatusId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ClaimPendingReasonId).GreaterThan(0);
        RuleFor(x => x.PendingReason).MaximumLength(300);
    }
}
