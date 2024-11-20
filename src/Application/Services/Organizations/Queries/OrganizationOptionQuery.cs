namespace Engage.Application.Services.Organizations.Queries;

public class OrganizationOptionQuery : IRequest<List<OrganizationOption>>
{ 

}

public record OrganizationOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<OrganizationOptionQuery, List<OrganizationOption>>
{
    public async Task<List<OrganizationOption>> Handle(OrganizationOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.Organizations.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.OrganizationId)
                              .ProjectTo<OrganizationOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}