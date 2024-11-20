namespace Engage.Application.Services.SparProductStatuses.Queries;

public class SparProductStatusOptionQuery : IRequest<List<SparProductStatusOption>>
{ 

}

public record SparProductStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductStatusOptionQuery, List<SparProductStatusOption>>
{
    public async Task<List<SparProductStatusOption>> Handle(SparProductStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparProductStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparProductStatusId)
                              .ProjectTo<SparProductStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}