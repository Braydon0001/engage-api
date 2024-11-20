// auto-generated
namespace Engage.Application.Services.InventoryGroups.Queries;

public class InventoryGroupListQuery : IRequest<List<InventoryGroupDto>>
{

}

public class InventoryGroupListHandler : ListQueryHandler, IRequestHandler<InventoryGroupListQuery, List<InventoryGroupDto>>
{
    public InventoryGroupListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryGroupDto>> Handle(InventoryGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<InventoryGroupDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}