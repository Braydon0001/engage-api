// auto-generated
namespace Engage.Application.Services.InventoryGroups.Queries;

public class InventoryGroupVmQuery : IRequest<InventoryGroupVm>
{
    public int Id { get; set; }
}

public class InventoryGroupVmHandler : VmQueryHandler, IRequestHandler<InventoryGroupVmQuery, InventoryGroupVm>
{
    public InventoryGroupVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryGroupVm> Handle(InventoryGroupVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryGroups.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryGroupId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<InventoryGroupVm>(entity);
    }
}