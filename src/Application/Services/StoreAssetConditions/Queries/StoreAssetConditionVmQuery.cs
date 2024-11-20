// auto-generated
namespace Engage.Application.Services.StoreAssetConditions.Queries;

public class StoreAssetConditionVmQuery : IRequest<StoreAssetConditionVm>
{
    public int Id { get; set; }
}

public class StoreAssetConditionVmHandler : VmQueryHandler, IRequestHandler<StoreAssetConditionVmQuery, StoreAssetConditionVm>
{
    public StoreAssetConditionVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreAssetConditionVm> Handle(StoreAssetConditionVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreAssetConditions.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.StoreAssetConditionId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<StoreAssetConditionVm>(entity);
    }
}