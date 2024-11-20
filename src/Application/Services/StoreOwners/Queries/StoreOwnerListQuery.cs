// auto-generated
namespace Engage.Application.Services.StoreOwners.Queries;

public class StoreOwnerListQuery : IRequest<List<StoreOwnerDto>>
{

}

public class StoreOwnerListHandler : ListQueryHandler, IRequestHandler<StoreOwnerListQuery, List<StoreOwnerDto>>
{
    public StoreOwnerListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreOwnerDto>> Handle(StoreOwnerListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreOwners.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.StartDate)
                              .ThenBy(e => e.EndDate)
                              .ThenBy(e => e.StoreOwnerId)
                              .ProjectTo<StoreOwnerDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}