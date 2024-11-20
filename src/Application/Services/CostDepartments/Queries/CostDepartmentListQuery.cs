namespace Engage.Application.Services.CostDepartments.Queries;

public class CostDepartmentListQuery : IRequest<List<CostDepartmentDto>>
{

}

public record CostDepartmentListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostDepartmentListQuery, List<CostDepartmentDto>>
{
    public async Task<List<CostDepartmentDto>> Handle(CostDepartmentListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostDepartments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CostDepartmentDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}