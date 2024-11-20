namespace Engage.Application.Services.SparUnitTypes.Queries;

public class SparUnitTypeListQuery : IRequest<List<SparUnitTypeDto>>
{

}

public record SparUnitTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparUnitTypeListQuery, List<SparUnitTypeDto>>
{
    public async Task<List<SparUnitTypeDto>> Handle(SparUnitTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparUnitTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparUnitTypeId)
                              .ProjectTo<SparUnitTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}