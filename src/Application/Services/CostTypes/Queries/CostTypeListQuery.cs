namespace Engage.Application.Services.CostTypes.Queries;

public class CostTypeListQuery : IRequest<List<CostTypeDto>>
{

}

public record CostTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostTypeListQuery, List<CostTypeDto>>
{
    public async Task<List<CostTypeDto>> Handle(CostTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CostTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}