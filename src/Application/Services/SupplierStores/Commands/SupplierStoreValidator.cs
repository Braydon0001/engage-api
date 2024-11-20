namespace Engage.Application.Services.SupplierStores.Commands;

public class SupplierStoreValidator<T> : AbstractValidator<T> where T : SupplierStoreCommand
{
    public SupplierStoreValidator()
    {
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SupplierRegionId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageSubGroupId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FrequencyTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Frequency).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AccountNumber).MaximumLength(120);
    }
}

public class CreateSupplierStoreValidator : SupplierStoreValidator<CreateSupplierStoreCommand>
{
}

public class UpdateSupplierStoreValidator : AbstractValidator<UpdateSupplierStoreCommand>
{
    public UpdateSupplierStoreValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SupplierRegionId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FrequencyTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Frequency).GreaterThanOrEqualTo(0);
        RuleFor(x => x.AccountNumber).MaximumLength(120);
    }
}
