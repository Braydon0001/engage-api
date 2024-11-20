namespace Engage.Application.Services.EmployeeStores.Commands
{
    public record StoreSubGroup(int StoreId, int EngageSubGroupId);

    public class StoreSubGroups
    {
        public static async Task<IEnumerable<StoreSubGroup>> Calculate(int fromEmployeeId, int toEmployeeId, IAppDbContext context, CancellationToken cancellationToken)
        {
            var toEmployeeRecords = await context.EmployeeStores.IgnoreQueryFilters()
                                                                .Where(e => e.EmployeeId == toEmployeeId)
                                                                .Select(e => new StoreSubGroup(e.StoreId, e.EngageSubGroupId))
                                                                .ToListAsync(cancellationToken);

            var fromEmployeeRecords = await context.EmployeeStores.IgnoreQueryFilters()
                                                                  .Where(e => e.EmployeeId == fromEmployeeId)
                                                                  .Select(e => new StoreSubGroup(e.StoreId, e.EngageSubGroupId))
                                                                  .ToListAsync(cancellationToken);

            return toEmployeeRecords.ExceptBy(fromEmployeeRecords, e => new StoreSubGroup(e.StoreId, e.EngageSubGroupId));
        }
    }
}
