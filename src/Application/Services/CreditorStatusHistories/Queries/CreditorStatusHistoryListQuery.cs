namespace Engage.Application.Services.CreditorStatusHistories.Queries;

public class CreditorStatusHistoryListQuery : IRequest<List<CreditorStatusHistoryDto>>
{

}

public record CreditorStatusHistoryListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusHistoryListQuery, List<CreditorStatusHistoryDto>>
{
    public async Task<List<CreditorStatusHistoryDto>> Handle(CreditorStatusHistoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorStatusHistories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CreditorStatusHistoryId)
                              .ProjectTo<CreditorStatusHistoryDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}