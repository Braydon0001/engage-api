namespace Engage.Application.Services.CommunicationTemplates.Queries;

public class CommunicationTemplateOptionQuery : IRequest<List<CommunicationTemplateOption>>
{

}

public record CommunicationTemplateOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateOptionQuery, List<CommunicationTemplateOption>>
{
    public async Task<List<CommunicationTemplateOption>> Handle(CommunicationTemplateOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationTemplates.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.CommunicationTemplateId)
                              .ProjectTo<CommunicationTemplateOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}