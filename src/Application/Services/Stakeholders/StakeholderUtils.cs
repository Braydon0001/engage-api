using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.Stores.Queries;
using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.Stakeholders;

public static class StakeholderUtils
{
    public static async Task<int> GetIdForType(IMediator mediator, StakeholderTypes stakeholderType, int entityId)
    {
        if (entityId <= 0)
        {
            throw new ArgumentException("stakeholderEntityId is less than or equal to zero");
        } 

        switch (stakeholderType)
        {
            case StakeholderTypes.Supplier:
                var supplier = await mediator.Send(new SupplierQuery { Id = entityId });
                return supplier.StakeholderId;
            case StakeholderTypes.Employee:
                var employee = await mediator.Send(new EmployeeQuery { Id = entityId });
                return employee.StakeholderId;
            case StakeholderTypes.Store:
                var store = await mediator.Send(new GetStoreQuery { Id = entityId });
                return store.StakeholderId;
            default:
                throw new InvalidStakeholderTypeException(Enum.GetName(typeof(StakeholderTypes), stakeholderType));
        }
    }
}
