namespace Engage.Application.Services.CommunicationTypes.Queries;

public class CommunicationTypeListQuery : IRequest<List<CommunicationTypeDto>>
{

}

public record CommunicationTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTypeListQuery, List<CommunicationTypeDto>>
{
    public async Task<List<CommunicationTypeDto>> Handle(CommunicationTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.CommunicationTypeId)
                              .ProjectTo<CommunicationTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}