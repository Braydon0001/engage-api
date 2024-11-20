namespace Engage.Application.Services.CostDepartments.Queries;

public record CostDepartmentVmQuery(int Id) : IRequest<CostDepartmentVm>;

public record CostDepartmentVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostDepartmentVmQuery, CostDepartmentVm>
{
    public async Task<CostDepartmentVm> Handle(CostDepartmentVmQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostDepartments.AsQueryable().AsNoTracking();
        
        var entity = await queryable.SingleOrDefaultAsync(e => e.CostDepartmentId == query.Id, cancellationToken);

        return entity == null ? null : Mapper.Map<CostDepartmentVm>(entity);
    }
}