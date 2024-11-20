namespace Engage.Application.Services.SecurityOrganizations.Queries;

public class SecurityOrganizationListQuery : IRequest<List<SecurityOrganizationDto>>
{

}

public record SecurityOrganizationListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SecurityOrganizationListQuery, List<SecurityOrganizationDto>>
{
    public async Task<List<SecurityOrganizationDto>> Handle(SecurityOrganizationListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.SecurityOrganizations.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.SecurityOrganizationId)
                              .ProjectTo<SecurityOrganizationDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}