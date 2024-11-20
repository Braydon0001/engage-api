namespace Engage.Application.Services.CostSubDepartments.Queries;

public class CostSubDepartmentOptionQuery : IRequest<List<CostSubDepartmentOption>>
{ 

}

public record CostSubDepartmentOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostSubDepartmentOptionQuery, List<CostSubDepartmentOption>>
{
    public async Task<List<CostSubDepartmentOption>> Handle(CostSubDepartmentOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostSubDepartments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CostSubDepartmentOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}