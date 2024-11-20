// auto-generated
namespace Engage.Application.Services.InventoryStatuses.Queries;

public class InventoryStatusVmQuery : IRequest<InventoryStatusVm>
{
    public int Id { get; set; }
}

public class InventoryStatusVmHandler : VmQueryHandler, IRequestHandler<InventoryStatusVmQuery, InventoryStatusVm>
{
    public InventoryStatusVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<InventoryStatusVm> Handle(InventoryStatusVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.InventoryStatuses.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.InventoryStatusId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<InventoryStatusVm>(entity);
    }
}