namespace Engage.Application.Services.EmployeeStores.Commands
{
    public record EmployeeSubGroup(int EmployeeId, int EngageSubGroupId);

    public class EmployeeSubGroups
    {
        public static async Task<IEnumerable<EmployeeSubGroup>> Calculate(int storeId, List<int> employeeIds, List<int> engageSubGroupIds, IAppDbContext context, CancellationToken cancellationToken)
        {
            var newRecords = new List<EmployeeSubGroup>();
            foreach (var employeeId in employeeIds)
            {
                foreach (var subgroupId in engageSubGroupIds)
                {
                    newRecords.Add(new EmployeeSubGroup(employeeId, subgroupId));
                }
            }

            var records = await context.EmployeeStores.IgnoreQueryFilters()
                                                      .Where(e => e.StoreId == storeId &&
                                                                  employeeIds.Contains(e.EmployeeId) &&
                                                                  engageSubGroupIds.Contains(e.EngageSubGroupId))
                                                      .Select(e => new EmployeeSubGroup(e.StoreId, e.EngageSubGroupId))
                                                      .ToListAsync(cancellationToken);

            return newRecords.ExceptBy(records, e => new EmployeeSubGroup(e.EmployeeId, e.EngageSubGroupId));
        }
    }
}
