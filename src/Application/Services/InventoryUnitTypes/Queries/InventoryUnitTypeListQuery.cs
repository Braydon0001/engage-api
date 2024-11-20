// auto-generated
namespace Engage.Application.Services.InventoryUnitTypes.Queries;

public class InventoryUnitTypeListQuery : IRequest<List<InventoryUnitTypeDto>>
{

}

public class InventoryUnitTypeListHandler : ListQueryHandler, IRequestHandler<InventoryUnitTypeListQuery, List<InventoryUnitTypeDto>>
{
    public InventoryUnitTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryUnitTypeDto>> Handle(InventoryUnitTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryUnitTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryUnitTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}