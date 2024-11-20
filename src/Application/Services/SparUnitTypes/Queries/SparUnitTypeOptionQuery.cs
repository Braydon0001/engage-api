namespace Engage.Application.Services.SparUnitTypes.Queries;

public class SparUnitTypeOptionQuery : IRequest<List<SparUnitTypeOption>>
{ 

}

public record SparUnitTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparUnitTypeOptionQuery, List<SparUnitTypeOption>>
{
    public async Task<List<SparUnitTypeOption>> Handle(SparUnitTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparUnitTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparUnitTypeId)
                              .ProjectTo<SparUnitTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}