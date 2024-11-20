namespace Engage.Application.Services.EntityContacts.Commands;

public class EntityContactValidator<T> : AbstractValidator<T> where T : EntityContactCommand
{
    public EntityContactValidator()
    {
        RuleFor(x => x.EntityContactTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.EmailAddress1).MaximumLength(100).NotEmpty();
        RuleFor(x => x.FirstName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.LastName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.MobilePhone).MaximumLength(30);
        RuleFor(x => x.Description).MaximumLength(200);
    }
}

public class CreateEngageRegionContactCommandValidator : EntityContactValidator<CreateEngageRegionContactCommand>
{
    public CreateEngageRegionContactCommandValidator()
    {
        RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
    }
}

public class CreateStoreContactCommandValidator : EntityContactValidator<CreateStoreContactCommand>
{
    public CreateStoreContactCommandValidator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
    }
}

public class CreateSupplierContactCommandValidator : EntityContactValidator<CreateSupplierContactCommand>
{
    public CreateSupplierContactCommandValidator()
    {
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateEngageRegionContactValidator : EntityContactValidator<UpdateEngageRegionContactCommand>
{
    public UpdateEngageRegionContactValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageRegionId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateStoreContactValidator : EntityContactValidator<UpdateStoreContactCommand>
{
    public UpdateStoreContactValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
    }
}

public class UpdateSupplierContactValidator : EntityContactValidator<UpdateSupplierContactCommand>
{
    public UpdateSupplierContactValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.SupplierId).GreaterThan(0).NotEmpty();
    }
}
