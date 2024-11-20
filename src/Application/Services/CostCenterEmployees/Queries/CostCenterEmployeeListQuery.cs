namespace Engage.Application.Services.CostCenterEmployees.Queries;

public class CostCenterEmployeeListQuery : IRequest<List<CostCenterEmployeeDto>>
{

}

public record CostCenterEmployeeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterEmployeeListQuery, List<CostCenterEmployeeDto>>
{
    public async Task<List<CostCenterEmployeeDto>> Handle(CostCenterEmployeeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostCenterEmployees.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CostCenterEmployeeId)
                              .ProjectTo<CostCenterEmployeeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}