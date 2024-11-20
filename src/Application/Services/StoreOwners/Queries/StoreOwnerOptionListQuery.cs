// auto-generated
namespace Engage.Application.Services.StoreOwners.Queries;

public class StoreOwnerOptionListQuery : IRequest<List<StoreOwnerOption>>
{ 

}

public class StoreOwnerOptionListHandler : ListQueryHandler, IRequestHandler<StoreOwnerOptionListQuery, List<StoreOwnerOption>>
{
    public StoreOwnerOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<StoreOwnerOption>> Handle(StoreOwnerOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreOwners.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.StartDate)
                              .ThenBy(e => e.EndDate)
                              .ThenBy(e => e.StoreOwnerId)
                              .ProjectTo<StoreOwnerOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}