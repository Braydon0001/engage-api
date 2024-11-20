namespace Engage.Application.Services.InventoryYears.Queries;

public class InventoryYearListQuery : IRequest<List<InventoryYearDto>>
{

}

public record InventoryYearListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<InventoryYearListQuery, List<InventoryYearDto>>
{
    public async Task<List<InventoryYearDto>> Handle(InventoryYearListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.InventoryYears.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryYearDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}