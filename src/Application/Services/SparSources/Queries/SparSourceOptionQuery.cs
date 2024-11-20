namespace Engage.Application.Services.SparSources.Queries;

public class SparSourceOptionQuery : IRequest<List<SparSourceOption>>
{ 

}

public record SparSourceOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSourceOptionQuery, List<SparSourceOption>>
{
    public async Task<List<SparSourceOption>> Handle(SparSourceOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSources.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparSourceId)
                              .ProjectTo<SparSourceOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}