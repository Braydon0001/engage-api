// auto-generated
namespace Engage.Application.Services.InventoryGroups.Queries;

public class InventoryGroupOptionListQuery : IRequest<List<InventoryGroupOption>>
{ 

}

public class InventoryGroupOptionListHandler : ListQueryHandler, IRequestHandler<InventoryGroupOptionListQuery, List<InventoryGroupOption>>
{
    public InventoryGroupOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryGroupOption>> Handle(InventoryGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryGroupOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}