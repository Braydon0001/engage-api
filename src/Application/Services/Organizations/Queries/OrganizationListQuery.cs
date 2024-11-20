namespace Engage.Application.Services.Organizations.Queries;

public class OrganizationListQuery : IRequest<List<OrganizationDto>>
{

}

public record OrganizationListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganizationListQuery, List<OrganizationDto>>
{
    public async Task<List<OrganizationDto>> Handle(OrganizationListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Organizations.AsQueryable().AsNoTracking();

        var entities = await queryable.OrderBy(e => e.OrganizationId)
                              .ProjectTo<OrganizationDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);

        return entities;
    }
}