// auto-generated
namespace Engage.Application.Services.StoreOwners.Queries;

public class StoreOwnerVmQuery : IRequest<StoreOwnerVm>
{
    public int Id { get; set; }
}

public class StoreOwnerVmHandler : VmQueryHandler, IRequestHandler<StoreOwnerVmQuery, StoreOwnerVm>
{
    public StoreOwnerVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreOwnerVm> Handle(StoreOwnerVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreOwners.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Store)
                             .Include(e => e.StoreGroup)
                             .Include(e => e.StoreOwnerType);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.StoreOwnerId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<StoreOwnerVm>(entity);
    }
}