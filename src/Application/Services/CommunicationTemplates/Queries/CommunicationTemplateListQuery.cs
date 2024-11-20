namespace Engage.Application.Services.CommunicationTemplates.Queries;

public class CommunicationTemplateListQuery : IRequest<List<CommunicationTemplateDto>>
{
    public int? CommunicationTypeId { get; set; }
}

public record CommunicationTemplateListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateListQuery, List<CommunicationTemplateDto>>
{
    public async Task<List<CommunicationTemplateDto>> Handle(CommunicationTemplateListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationTemplates.AsQueryable().AsNoTracking();

        if (query.CommunicationTypeId.HasValue)
        {
            queryable = queryable.Where(c => c.CommunicationTypeId == query.CommunicationTypeId.Value);
        }

        return await queryable.OrderBy(e => e.CommunicationTemplateId)
                              .ProjectTo<CommunicationTemplateDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}