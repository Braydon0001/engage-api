namespace Engage.Application.Services.SparProductStatuses.Queries;

public class SparProductStatusListQuery : IRequest<List<SparProductStatusDto>>
{

}

public record SparProductStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductStatusListQuery, List<SparProductStatusDto>>
{
    public async Task<List<SparProductStatusDto>> Handle(SparProductStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparProductStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparProductStatusId)
                              .ProjectTo<SparProductStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}