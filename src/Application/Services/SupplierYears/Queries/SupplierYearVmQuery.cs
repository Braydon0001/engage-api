namespace Engage.Application.Services.SupplierYears.Queries;

public class SupplierYearVmQuery : IRequest<SupplierYearVm>
{
    public int Id { get; set; }
}

public class SupplierYearVmHandler : VmQueryHandler, IRequestHandler<SupplierYearVmQuery, SupplierYearVm>
{
    public SupplierYearVmHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<SupplierYearVm> Handle(SupplierYearVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.SupplierYears.AsQueryable().AsNoTracking();

        var entity = await queryable.SingleOrDefaultAsync(e => e.SupplierYearId == query.Id, cancellationToken);

        return entity == null ? null : _mapper.Map<SupplierYearVm>(entity);
    }
}
