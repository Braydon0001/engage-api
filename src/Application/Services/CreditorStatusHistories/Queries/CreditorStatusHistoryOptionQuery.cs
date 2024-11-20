namespace Engage.Application.Services.CreditorStatusHistories.Queries;

public class CreditorStatusHistoryOptionQuery : IRequest<List<CreditorStatusHistoryOption>>
{ 

}

public record CreditorStatusHistoryOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusHistoryOptionQuery, List<CreditorStatusHistoryOption>>
{
    public async Task<List<CreditorStatusHistoryOption>> Handle(CreditorStatusHistoryOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorStatusHistories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CreditorStatusHistoryId)
                              .ProjectTo<CreditorStatusHistoryOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}