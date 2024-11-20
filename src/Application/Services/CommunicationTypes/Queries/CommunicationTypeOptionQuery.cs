namespace Engage.Application.Services.CommunicationTypes.Queries;

public class CommunicationTypeOptionQuery : IRequest<List<CommunicationTypeOption>>
{

}

public record CommunicationTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTypeOptionQuery, List<CommunicationTypeOption>>
{
    public async Task<List<CommunicationTypeOption>> Handle(CommunicationTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.CommunicationTypeId)
                              .ProjectTo<CommunicationTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}