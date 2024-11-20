namespace Engage.Application.Services.EmployeeStores.Commands;

public class CreateEmployeeStoresByEmployeeCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public List<int> StoreIds { get; set; }
    public List<int> EngageDepartmentIds { get; set; }
    public List<int> EngageSubGroupIds { get; set; }
    public int FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string Note { get; set; }
}

public class CreateEmployeeStoresCommandHandler : IRequestHandler<CreateEmployeeStoresByEmployeeCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public CreateEmployeeStoresCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(CreateEmployeeStoresByEmployeeCommand command, CancellationToken cancellationToken)
    {
        var subGroupIds = await CalculateSubGroupIds(command, cancellationToken);

        // Delete the existing rows if we are setting the engage departments 
        if (command.EngageDepartmentIds != null && command.EngageDepartmentIds.Count > 0)
        {
            var oldEmployeeStores = await _context.EmployeeStores.IgnoreQueryFilters()
                                                             .Where(e => e.EmployeeId == command.EmployeeId &&
                                                                          !(command.StoreIds.Contains(e.StoreId) &&
                                                                            subGroupIds.Contains(e.EngageSubGroupId)))
                                                             .ToListAsync(cancellationToken);

            _context.EmployeeStores.RemoveRange(oldEmployeeStores);
        }

        // Add new records
        var storeSubGroups = new List<StoreSubGroup>();
        foreach (var storeId in command.StoreIds)
        {
            foreach (var subgroupId in subGroupIds)
            {
                storeSubGroups.Add(new StoreSubGroup(storeId, subgroupId));
            }
        }

        var existingStoreSubGroups = await _context.EmployeeStores.IgnoreQueryFilters()
                                                                  .Where(e => e.EmployeeId == command.EmployeeId &&
                                                                              command.StoreIds.Contains(e.StoreId) &&
                                                                              subGroupIds.Contains(e.EngageSubGroupId))
                                                                  .Select(e => new StoreSubGroup(e.StoreId, e.EngageSubGroupId))
                                                                  .ToListAsync(cancellationToken);

        var newStoreSubGroups = storeSubGroups.ExceptBy(existingStoreSubGroups, e => new StoreSubGroup(e.StoreId, e.EngageSubGroupId))
                                              .ToList();

        foreach (var storeSubGroup in newStoreSubGroups)
        {
            _context.EmployeeStores.Add(new EmployeeStore
            {
                EmployeeId = command.EmployeeId,
                StoreId = storeSubGroup.StoreId,
                EngageSubGroupId = storeSubGroup.EngageSubGroupId,
                FrequencyTypeId = command.FrequencyTypeId,
                Frequency = command.Frequency,
                Note = command.Note,
            });
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task<List<int>> CalculateSubGroupIds(CreateEmployeeStoresByEmployeeCommand command, CancellationToken cancellationToken)
    {
        var subGroupsQueryable = _context.EngageSubGroups.Where(e => e.Disabled == false);
        if (command.EngageDepartmentIds != null && command.EngageDepartmentIds.Count > 0)
        {
            subGroupsQueryable = subGroupsQueryable.Where(e => command.EngageDepartmentIds.Contains(e.EngageDepartmentId));
        }
        if (command.EngageSubGroupIds != null && command.EngageSubGroupIds.Count > 0)
        {
            subGroupsQueryable = subGroupsQueryable.Where(e => command.EngageSubGroupIds.Contains(e.Id));
        }
        return await subGroupsQueryable.Select(e => e.Id)
                                       .Distinct()
                                       .ToListAsync(cancellationToken);
    }
}
