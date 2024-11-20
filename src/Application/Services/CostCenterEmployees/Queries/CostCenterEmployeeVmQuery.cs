namespace Engage.Application.Services.CostCenterEmployees.Queries;

public record CostCenterEmployeeVmQuery(int Id) : IRequest<CostCenterEmployeeVm>;

public record CostCenterEmployeeVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterEmployeeVmQuery, CostCenterEmployeeVm>
{
    public async Task<CostCenterEmployeeVm> Handle(CostCenterEmployeeVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostCenterEmployees.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.CostCenter)
                             .Include(e => e.Employee);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CostCenterEmployeeId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CostCenterEmployeeVm>(entity);
    }
}