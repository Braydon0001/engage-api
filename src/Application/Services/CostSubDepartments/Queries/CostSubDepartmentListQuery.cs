namespace Engage.Application.Services.CostSubDepartments.Queries;

public class CostSubDepartmentListQuery : IRequest<List<CostSubDepartmentDto>>
{

}

public record CostSubDepartmentListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostSubDepartmentListQuery, List<CostSubDepartmentDto>>
{
    public async Task<List<CostSubDepartmentDto>> Handle(CostSubDepartmentListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostSubDepartments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CostSubDepartmentDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}