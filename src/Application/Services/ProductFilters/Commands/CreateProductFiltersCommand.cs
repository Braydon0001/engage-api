namespace Engage.Application.Services.ProductFilters.Commands;

public record FilterRecord(int ProductId, string Filter);

public class CreateProductFiltersCommand : IRequest<OperationStatus>
{
    public List<int> ProductIds { get; set; }
    public string Filter { get; set; }
}

public class CreateProductFiltersCommandHandler : IRequestHandler<CreateProductFiltersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public CreateProductFiltersCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(CreateProductFiltersCommand command, CancellationToken cancellationToken)
    {
        var filters = await CalculateNewFilters(command, _context, cancellationToken);

        foreach (var filter in filters)
        {
            _context.ProductFilters.Add(new ProductFilter
            {
                EngageVariantProductId = filter.ProductId,
                Filter = filter.Filter,
            });
        }

        return await _context.SaveChangesAsync(cancellationToken);

        throw new Exception();
    }

    private async Task<IEnumerable<FilterRecord>> CalculateNewFilters(CreateProductFiltersCommand command, IAppDbContext context, CancellationToken cancellationToken)
    {
        var newFilters = new List<FilterRecord>();
        foreach (var ProductId in command.ProductIds)
        {

            newFilters.Add(new FilterRecord(ProductId, command.Filter));

        }

        var filters = await context.ProductFilters.IgnoreQueryFilters()
                                                .Where(e => command.ProductIds.Contains(e.EngageVariantProductId.Value) &&
                                                            command.Filter.Contains(e.Filter))
                                                .Select(e => new FilterRecord(e.EngageVariantProductId.Value, e.Filter))
                                                .ToListAsync(cancellationToken);


        return newFilters.ExceptBy(filters, e => new FilterRecord(e.ProductId, e.Filter));
    }
}
