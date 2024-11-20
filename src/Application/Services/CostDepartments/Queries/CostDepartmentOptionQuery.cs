namespace Engage.Application.Services.CostDepartments.Queries;

public class CostDepartmentOptionQuery : IRequest<List<CostDepartmentOption>>
{ 

}

public record CostDepartmentOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostDepartmentOptionQuery, List<CostDepartmentOption>>
{
    public async Task<List<CostDepartmentOption>> Handle(CostDepartmentOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostDepartments.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CostDepartmentOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}