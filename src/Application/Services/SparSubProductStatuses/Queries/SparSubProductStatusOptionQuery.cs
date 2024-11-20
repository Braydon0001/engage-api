namespace Engage.Application.Services.SparSubProductStatuses.Queries;

public class SparSubProductStatusOptionQuery : IRequest<List<SparSubProductStatusOption>>
{ 

}

public record SparSubProductStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSubProductStatusOptionQuery, List<SparSubProductStatusOption>>
{
    public async Task<List<SparSubProductStatusOption>> Handle(SparSubProductStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSubProductStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparSubProductStatusId)
                              .ProjectTo<SparSubProductStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}