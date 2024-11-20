namespace Engage.Application.Services.SparSystemStatuses.Queries;

public class SparSystemStatusListQuery : IRequest<List<SparSystemStatusDto>>
{

}

public record SparSystemStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSystemStatusListQuery, List<SparSystemStatusDto>>
{
    public async Task<List<SparSystemStatusDto>> Handle(SparSystemStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSystemStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparSystemStatusId)
                              .ProjectTo<SparSystemStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}