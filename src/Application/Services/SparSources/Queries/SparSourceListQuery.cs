namespace Engage.Application.Services.SparSources.Queries;

public class SparSourceListQuery : IRequest<List<SparSourceDto>>
{

}

public record SparSourceListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSourceListQuery, List<SparSourceDto>>
{
    public async Task<List<SparSourceDto>> Handle(SparSourceListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSources.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparSourceId)
                              .ProjectTo<SparSourceDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}