namespace Engage.Application.Services.CostCenterDepartments.Queries;

public class CostCenterDepartmentListQuery : IRequest<List<CostCenterDepartmentDto>>
{

}

public record CostCenterDepartmentListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterDepartmentListQuery, List<CostCenterDepartmentDto>>
{
    public async Task<List<CostCenterDepartmentDto>> Handle(CostCenterDepartmentListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostCenterDepartments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CostCenterDepartmentId)
                              .ProjectTo<CostCenterDepartmentDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}