namespace Engage.Application.Services.StoreFilters.Commands;

public record FilterRecord(int StoreId, string Filter);

public class CreateStoreFiltersCommand : IRequest<OperationStatus>
{
    public List<int> StoreIds { get; set; }
    public string Filter { get; set; }
}

public class CreateStoreFiltersCommandHandler : IRequestHandler<CreateStoreFiltersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public CreateStoreFiltersCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(CreateStoreFiltersCommand command, CancellationToken cancellationToken)
    {
        if (command.StoreIds == null || !command.StoreIds.Any())
        {
            throw new Exception("Stores cannot be empty! Please select at least 1 store.");
        }

        var filters = await CalculateNewFilters(command, _context, cancellationToken);

        foreach (var filter in filters)
        {
            _context.StoreFilters.Add(new StoreFilter
            {
                StoreId = filter.StoreId,
                Filter = filter.Filter,
            });
        }

        return await _context.SaveChangesAsync(cancellationToken);

        throw new Exception();
    }

    private async Task<IEnumerable<FilterRecord>> CalculateNewFilters(CreateStoreFiltersCommand command, IAppDbContext context, CancellationToken cancellationToken)
    {
        var newFilters = new List<FilterRecord>();
        foreach (var storeId in command.StoreIds)
        {

            newFilters.Add(new FilterRecord(storeId, command.Filter));
        }

        var filters = await context.StoreFilters.IgnoreQueryFilters()
                                                .Where(e => command.StoreIds.Contains(e.StoreId) &&
                                                            command.Filter.Contains(e.Filter))
                                                .Select(e => new FilterRecord(e.StoreId, e.Filter))
                                                .ToListAsync(cancellationToken);


        return newFilters.ExceptBy(filters, e => new FilterRecord(e.StoreId, e.Filter));
    }
}
