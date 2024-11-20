// auto-generated
namespace Engage.Application.Services.InventoryStatuses.Queries;

public class InventoryStatusOptionListQuery : IRequest<List<InventoryStatusOption>>
{ 

}

public class InventoryStatusOptionListHandler : ListQueryHandler, IRequestHandler<InventoryStatusOptionListQuery, List<InventoryStatusOption>>
{
    public InventoryStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryStatusOption>> Handle(InventoryStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}