namespace Engage.Application.Services.CostTypes.Queries;

public class CostTypeOptionQuery : IRequest<List<CostTypeOption>>
{ 

}

public record CostTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostTypeOptionQuery, List<CostTypeOption>>
{
    public async Task<List<CostTypeOption>> Handle(CostTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CostTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CostTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}