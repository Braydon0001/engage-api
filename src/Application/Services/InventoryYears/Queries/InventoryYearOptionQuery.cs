namespace Engage.Application.Services.InventoryYears.Queries;

public class InventoryYearOptionQuery : IRequest<List<InventoryYearOption>>
{ 

}

public record InventoryYearOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryYearOptionQuery, List<InventoryYearOption>>
{
    public async Task<List<InventoryYearOption>> Handle(InventoryYearOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.InventoryYears.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryYearOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}