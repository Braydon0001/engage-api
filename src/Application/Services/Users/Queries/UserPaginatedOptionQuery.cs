namespace Engage.Application.Services.Users.Queries;

public class UserPaginatedOptionQuery : PaginatedQuery, IRequest<List<UserOption>>
{
}

public class UserPaginatedOptionListHandler : ListQueryHandler, IRequestHandler<UserPaginatedOptionQuery, List<UserOption>>
{
    public UserPaginatedOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<UserOption>> Handle(UserPaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = UserPaginationProps.Create();

        var queryable = _context.Users.AsQueryable().AsNoTracking();

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.FullName);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<UserOption>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }
}
