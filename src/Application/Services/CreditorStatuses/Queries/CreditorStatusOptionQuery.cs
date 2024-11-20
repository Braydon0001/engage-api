namespace Engage.Application.Services.CreditorStatuses.Queries;

public class CreditorStatusOptionQuery : IRequest<List<CreditorStatusOption>>
{ 

}

public record CreditorStatusOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusOptionQuery, List<CreditorStatusOption>>
{
    public async Task<List<CreditorStatusOption>> Handle(CreditorStatusOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CreditorStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.CreditorStatusId)
                              .ProjectTo<CreditorStatusOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}