namespace Engage.Application.Services.EmployeeStores.Commands;

public class EmployeeStoreValidator<T> : AbstractValidator<T> where T : EmployeeStoreCommand
{
    public EmployeeStoreValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EngageSubGroupId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FrequencyTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Frequency).GreaterThanOrEqualTo(0);
    }
}

public class CreateEmployeeStoreValidator : EmployeeStoreValidator<CreateEmployeeStoreCommand>
{
}

public class UpdateEmployeeStoreValidator : AbstractValidator<UpdateEmployeeStoreCommand>
{
    public UpdateEmployeeStoreValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FrequencyTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Frequency).GreaterThanOrEqualTo(0);
    }
}

public class CreateEmployeeStoresByEmployeeValidator : AbstractValidator<CreateEmployeeStoresByEmployeeCommand>
{
    public CreateEmployeeStoresByEmployeeValidator()
    {
        RuleFor(x => x.EmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.StoreIds).ForEach(e => e.GreaterThan(0).NotEmpty());
        RuleFor(x => x.EngageSubGroupIds).ForEach(e => e.GreaterThan(0).NotEmpty());
        RuleFor(x => x.FrequencyTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Frequency).GreaterThanOrEqualTo(0);
    }
}
public class CreateEmployeeStoresByStoreValidator : AbstractValidator<CreateEmployeeStoresByStoreCommand>
{
    public CreateEmployeeStoresByStoreValidator()
    {
        RuleFor(x => x.StoreId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeId).ForEach(e => e.GreaterThan(0).NotEmpty());
        RuleFor(x => x.EngageSubGroupId).ForEach(e => e.GreaterThan(0).NotEmpty());
        RuleFor(x => x.FrequencyTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Frequency).GreaterThanOrEqualTo(0);
    }
}

public class CopyEmployeeStoresValidator : AbstractValidator<CopyEmployeeStoresCommand>
{
    public CopyEmployeeStoresValidator()
    {
        RuleFor(x => x.FromEmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.ToEmployeeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FrequencyTypeId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.Frequency).GreaterThanOrEqualTo(0);
    }
}