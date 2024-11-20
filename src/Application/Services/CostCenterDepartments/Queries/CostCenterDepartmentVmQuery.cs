namespace Engage.Application.Services.CostCenterDepartments.Queries;

public record CostCenterDepartmentVmQuery(int Id) : IRequest<CostCenterDepartmentVm>;

public record CostCenterDepartmentVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterDepartmentVmQuery, CostCenterDepartmentVm>
{
    public async Task<CostCenterDepartmentVm> Handle(CostCenterDepartmentVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostCenterDepartments.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.CostCenter)
                             .Include(e => e.CostDepartment);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CostCenterDepartmentId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CostCenterDepartmentVm>(entity);
    }
}