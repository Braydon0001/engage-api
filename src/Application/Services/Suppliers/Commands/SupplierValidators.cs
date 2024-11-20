namespace Engage.Application.Services.Suppliers.Commands;

public class SupplierValidator<T> : AbstractValidator<T> where T : SupplierCommand
{
    public SupplierValidator()
    {
        RuleFor(x => x.SupplierGroupId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.PrimaryLocationId).GreaterThan(0);
        RuleFor(x => x.PrimaryContactId).GreaterThan(0);
        RuleFor(x => x.Code).MaximumLength(30).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(120).NotEmpty();
        RuleFor(x => x.ShortName).MaximumLength(30);
        RuleFor(x => x.VATNumber).MaximumLength(15);
        RuleFor(x => x.ClaimAccountManagerId).GreaterThan(0);
        RuleFor(x => x.ClaimManagerId).GreaterThan(0);
        RuleFor(x => x.ClaimReportTitle).MaximumLength(200);
        RuleFor(x => x.ClaimReportAccountNumber).MaximumLength(200);
        RuleForEach(x => x.SupplierTypeIds).GreaterThan(0);
        RuleForEach(x => x.EngageBrandIds).GreaterThan(0);
    }
}

public class CreateSupplierValidator : SupplierValidator<CreateSupplierCommand>
{

}

public class UpdateSupplierValidator : SupplierValidator<UpdateSupplierCommand>
{
    public UpdateSupplierValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}
