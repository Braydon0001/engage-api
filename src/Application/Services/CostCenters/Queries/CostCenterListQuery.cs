namespace Engage.Application.Services.CostCenters.Queries;

public class CostCenterListQuery : IRequest<List<CostCenterDto>>
{

}

public record CostCenterListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterListQuery, List<CostCenterDto>>
{
    public async Task<List<CostCenterDto>> Handle(CostCenterListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostCenters.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CostCenterDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}