namespace Engage.Application.Services.CommunicationTemplateTypes.Queries;

public class CommunicationTemplateTypeOptionQuery : IRequest<List<CommunicationTemplateTypeOption>>
{

}

public record CommunicationTemplateTypeOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateTypeOptionQuery, List<CommunicationTemplateTypeOption>>
{
    public async Task<List<CommunicationTemplateTypeOption>> Handle(CommunicationTemplateTypeOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationTemplateTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<CommunicationTemplateTypeOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}