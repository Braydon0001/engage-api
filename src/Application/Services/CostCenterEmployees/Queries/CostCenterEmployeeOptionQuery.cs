namespace Engage.Application.Services.CostCenterEmployees.Queries;

public class CostCenterEmployeeOptionQuery : IRequest<List<CostCenterEmployeeOption>>
{ 

}

public record CostCenterEmployeeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterEmployeeOptionQuery, List<CostCenterEmployeeOption>>
{
    public async Task<List<CostCenterEmployeeOption>> Handle(CostCenterEmployeeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostCenterEmployees.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CostCenterEmployeeId)
                              .ProjectTo<CostCenterEmployeeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}