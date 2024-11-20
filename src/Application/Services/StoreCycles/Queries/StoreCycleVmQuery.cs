namespace Engage.Application.Services.StoreCycles.Queries;

public class StoreCycleVmQuery : IRequest<StoreCycleVm>
{
    public int Id { get; set; }
}

public class StoreCycleVmHandler : VmQueryHandler, IRequestHandler<StoreCycleVmQuery, StoreCycleVm>
{
    public StoreCycleVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<StoreCycleVm> Handle(StoreCycleVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.StoreCycles.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.Store)
                             .Include(e => e.EngageDepartment)
                             .Include(e => e.StoreCycleOperation)
                             .Include(e => e.FrequencyType);

        var entity = await queryable.SingleOrDefaultAsync(e => e.StoreCycleId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<StoreCycleVm>(entity);
    }
}