using Engage.Application.Services.Users.Models;

namespace Engage.Application.Services.Users.Queries;

public class UserPaginatedQuery : PaginatedQuery, IRequest<ListResult<UserDto>>
{
}

public record UserPaginatedHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<UserPaginatedQuery, ListResult<UserDto>>
{

    public async Task<ListResult<UserDto>> Handle(UserPaginatedQuery query, CancellationToken cancellationToken)
    {
        var props = UserPaginationProps.Create();

        var queryable = Context.Users.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.FirstName).ThenBy(e => e.LastName);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<UserDto>(Mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new(entities);
    }
}
