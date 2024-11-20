// auto-generated
namespace Engage.Application.Services.StoreAssetSubTypes.Queries;

public class StoreAssetSubTypeVmQuery : IRequest<StoreAssetSubTypeVm>
{
    public int Id { get; set; }
}

public class StoreAssetSubTypeVmHandler : VmQueryHandler, IRequestHandler<StoreAssetSubTypeVmQuery, StoreAssetSubTypeVm>
{
    public StoreAssetSubTypeVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreAssetSubTypeVm> Handle(StoreAssetSubTypeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreAssetSubTypes.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.StoreAssetTypes)
                             .ThenInclude(e => e.StoreAssetType);

        var entity = await queryable.SingleOrDefaultAsync(e => e.StoreAssetSubTypeId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<StoreAssetSubTypeVm>(entity);
    }
}