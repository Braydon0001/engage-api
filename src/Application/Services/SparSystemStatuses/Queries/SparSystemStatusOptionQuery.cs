namespace Engage.Application.Services.SparSystemStatuses.Queries;

public class SparSystemStatusOptionQuery : IRequest<List<SparSystemStatusOption>>
{ 

}

public record SparSystemStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSystemStatusOptionQuery, List<SparSystemStatusOption>>
{
    public async Task<List<SparSystemStatusOption>> Handle(SparSystemStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSystemStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparSystemStatusId)
                              .ProjectTo<SparSystemStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}