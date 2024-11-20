namespace Engage.Application.Services.UserStores.Queries;

public class UserStoreListQuery : IRequest<List<UserStoreDto>>
{
    public int UserId { get; set; }
}

public record UserStoreListHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserStoreListQuery, List<UserStoreDto>>
{
    public async Task<List<UserStoreDto>> Handle(UserStoreListQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId < 1)
        {
            throw new Exception("Project not found");
        }

        var user = await Context.Users.FindAsync(query.UserId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var queryable = Context.UserStores.Where(e => e.UserId == query.UserId).AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.UserStoreId)
                              .ProjectTo<UserStoreDto>(Mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}