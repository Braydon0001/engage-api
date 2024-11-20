namespace Engage.Application.Services.UserOrganizations.Queries;

public class UserOrganizationOptionQuery : IRequest<List<UserOrganizationOption>>
{ 

}

public record UserOrganizationOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationOptionQuery, List<UserOrganizationOption>>
{
    public async Task<List<UserOrganizationOption>> Handle(UserOrganizationOptionQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserOrganizations.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserOrganizationId)
                              .ProjectTo<UserOrganizationOption>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}