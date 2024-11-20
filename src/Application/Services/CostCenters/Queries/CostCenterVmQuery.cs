namespace Engage.Application.Services.CostCenters.Queries;

public record CostCenterVmQuery(int Id) : IRequest<CostCenterVm>;

public record CostCenterVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterVmQuery, CostCenterVm>
{
    public async Task<CostCenterVm> Handle(CostCenterVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostCenters.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.CostType)
                             .Include(e => e.CostCenterDepartments)
                                .ThenInclude(c => c.CostDepartment)
                             .Include(e => e.CostCenterEmployees)
                                .ThenInclude(c => c.Employee);

        var entity = await queryable.SingleOrDefaultAsync(e => e.CostCenterId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CostCenterVm>(entity);
    }
}