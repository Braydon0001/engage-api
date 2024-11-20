namespace Engage.Application.Services.SecurityOrganizations.Queries;

public class SecurityOrganizationOptionQuery : IRequest<List<SecurityOrganizationOption>>
{ 

}

public record SecurityOrganizationOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityOrganizationOptionQuery, List<SecurityOrganizationOption>>
{
    public async Task<List<SecurityOrganizationOption>> Handle(SecurityOrganizationOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityOrganizations.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SecurityOrganizationId)
                              .ProjectTo<SecurityOrganizationOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}