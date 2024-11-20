namespace Engage.Application.Services.SupplierPeriods.Queries;

public class SupplierPeriodVmQuery : IRequest<SupplierPeriodVm>
{
    public int Id { get; set; }
}

public class SupplierPeriodVmHandler : VmQueryHandler, IRequestHandler<SupplierPeriodVmQuery, SupplierPeriodVm>
{
    public SupplierPeriodVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierPeriodVm> Handle(SupplierPeriodVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierPeriods.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.SupplierYear);

        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierPeriodId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierPeriodVm>(entity);
    }
}
