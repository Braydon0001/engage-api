// auto-generated
namespace Engage.Application.Services.InventoryStatuses.Queries;

public class InventoryStatusListQuery : IRequest<List<InventoryStatusDto>>
{

}

public class InventoryStatusListHandler : ListQueryHandler, IRequestHandler<InventoryStatusListQuery, List<InventoryStatusDto>>
{
    public InventoryStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryStatusDto>> Handle(InventoryStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}