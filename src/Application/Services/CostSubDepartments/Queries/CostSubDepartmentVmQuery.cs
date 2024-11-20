namespace Engage.Application.Services.CostSubDepartments.Queries;

public record CostSubDepartmentVmQuery(int Id) : IRequest<CostSubDepartmentVm>;

public record CostSubDepartmentVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostSubDepartmentVmQuery, CostSubDepartmentVm>
{
    public async Task<CostSubDepartmentVm> Handle(CostSubDepartmentVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostSubDepartments.AsQueryable().AsNoTracking();

        queryable = queryable.Include(e => e.CostDepartment);
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CostSubDepartmentId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CostSubDepartmentVm>(entity);
    }
}