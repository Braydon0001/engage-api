namespace Engage.Application.Services.CommunicationTemplateTypes.Queries;

public class CommunicationTemplateTypeListQuery : IRequest<List<CommunicationTemplateTypeDto>>
{

}

public record CommunicationTemplateTypeListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTemplateTypeListQuery, List<CommunicationTemplateTypeDto>>
{
    public async Task<List<CommunicationTemplateTypeDto>> Handle(CommunicationTemplateTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.CommunicationTemplateTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.CommunicationTemplateTypeId)
                              .ProjectTo<CommunicationTemplateTypeDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}