// auto-generated
namespace Engage.Application.Services.InventoryUnitTypes.Queries;

public class InventoryUnitTypeOptionListQuery : IRequest<List<InventoryUnitTypeOption>>
{ 

}

public class InventoryUnitTypeOptionListHandler : ListQueryHandler, IRequestHandler<InventoryUnitTypeOptionListQuery, List<InventoryUnitTypeOption>>
{
    public InventoryUnitTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryUnitTypeOption>> Handle(InventoryUnitTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryUnitTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryUnitTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}