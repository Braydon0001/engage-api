namespace Engage.Application.Services.SparSubProductStatuses.Queries;

public class SparSubProductStatusListQuery : IRequest<List<SparSubProductStatusDto>>
{

}

public record SparSubProductStatusListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSubProductStatusListQuery, List<SparSubProductStatusDto>>
{
    public async Task<List<SparSubProductStatusDto>> Handle(SparSubProductStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SparSubProductStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SparSubProductStatusId)
                              .ProjectTo<SparSubProductStatusDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}