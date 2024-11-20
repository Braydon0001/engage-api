namespace Engage.Application.Services.UserOrganizations.Queries;

public class UserOrganizationListQuery : IRequest<List<UserOrganizationDto>>
{

}

public record UserOrganizationListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserOrganizationListQuery, List<UserOrganizationDto>>
{
    public async Task<List<UserOrganizationDto>> Handle(UserOrganizationListQuery query, CancellationToken cancellationToken)
    {
        var queryable = Context.UserOrganizations.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.UserOrganizationId)
                              .ProjectTo<UserOrganizationDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}